const tblProduct = "products";
const tblProductCart = "productCart";
let productId = null;

// createBlog("P1", "P1", "25");
getProductTable();
getCartTable();

function readProduct() {
  let lst = getProducts();
  console.log(lst);
}

function createProduct(name, description, price) {
  let lst = getProducts();

  const requestModel = {
    id: Date.now(),
    name: name,
    description: description,
    price: price,
  };

  lst.push(requestModel);

  const jsonProduct = JSON.stringify(lst);
  localStorage.setItem(tblProduct, jsonProduct);

  successMessage("Saving Succesful");

  clearControl();
}

function deleteProduct(id) {
  let lst = getProducts();

  const items = lst.filter((x) => x.id == id);
  if (items.length == 0) {
    errorMessage("No Data Found");
    return;
  }

  lst = lst.filter((x) => x.id != id);
  const jsonProduct = JSON.stringify(lst);
  localStorage.setItem(tblProduct, jsonProduct);

  successMessage("Data Deleted Successfully");

  getProductTable();
}

function editProduct(id) {
  let lst = getProducts();

  const items = lst.filter((x) => x.id == id);
  if (items.length == 0) {
    errorMessage("No Data Found");
    return;
  }

  let item = items[0];
  productId = item.id;
  $("#txtPname").val(item.name);
  $("#txtPdesc").val(item.description);
  $("#txtPprice").val(item.price);
}

function updateProduct(id, name, description, price) {
  let lst = getProducts();

  const items = lst.filter((x) => x.id == id);
  if (items.length == 0) {
    errorMessage("No Data Found");
    return;
  }

  const item = items[0];

  item.name = name;
  item.description = description;
  item.price = price;

  const index = lst.findIndex((x) => x.id == id);
  lst[index] = item;

  const jsonProduct = JSON.stringify(lst);
  localStorage.setItem(tblProduct, jsonProduct);

  successMessage("Updating Successful");
}

function getProducts() {
  const products = localStorage.getItem(tblProduct);
  console.log(products);

  let lst = [];
  if (products !== null) {
    lst = JSON.parse(products);
  }

  return lst;
}

function getProductTable() {
  const lst = getProducts();
  let count = 0;
  let htmlRows = "";
  lst.forEach((item) => {
    const htmlRow = `
    <tr>
      <td>${++count}</td>
      <td>${item.name}</td>
      <td>${item.description}</td>
      <td>${item.price}</td>
      <td>
        <button type="button" class="btn btn-warning" onclick="editProduct('${
          item.id
        }')">Edit</button>
        <button type="button" class="btn btn-danger" onclick="deleteProduct('${
          item.id
        }')">Delete</button>
      </td>
      <td>
        <button type="button" class="btn btn-dark" onclick="addProductCart('${
          item.id
        }')">Add to Cart</button>
      </td>
    </tr>
    `;
    htmlRows += htmlRow;
  });

  $("#tbody").html(htmlRows);
}

function clearControl() {
  $("#txtPname").val("");
  $("#txtPdesc").val("");
  $("#txtPprice").val("");

  $("#txtPname").focus();
}

$("#btnSave").click(function () {
  const name = $("#txtPname").val();
  const description = $("#txtPdesc").val();
  const price = $("#txtPprice").val();

  if (productId === null) {
    createProduct(name, description, price);
  } else {
    updateProduct(productId, name, description, price);
    clearControl();
    productId = null;
  }

  getProductTable();
});

$("#btnCancel").click(function () {
  clearControl();
});

/////////////////// Add to cart functions /////////////////////

function addProductCart(id) {
  let lst = getCartProducts();

  let productlst = getProducts();

  const items = productlst.filter((x) => x.id == id);
  if (items.length == 0) {
    errorMessage("No Data Found");
    return;
  }
  let item = items[0];

  const productsInCart = lst.filter((x) => x.id == id);
  if (productsInCart.length == 0) {
    const requestModel = {
      id: id,
      name: item.name,
      price: item.price,
      productQty: 1,
    };

    lst.push(requestModel);
  } else {
    let productInCart = productsInCart[0];
    productInCart.productQty += 1;
  }

  const jsonCartProduct = JSON.stringify(lst);
  localStorage.setItem(tblProductCart, jsonCartProduct);

  successMessage("Adding to cart succesful");

  getCartTable();
  clearControl();
}

function getCartProducts() {
  const cartProducts = localStorage.getItem(tblProductCart);
  console.log(cartProducts);

  let lst = [];
  if (cartProducts !== null) {
    lst = JSON.parse(cartProducts);
  }

  return lst;
}

function getCartTable() {
  const lst = getCartProducts();
  let count = 0;
  let htmlRows = "";

  lst.forEach((product) => {
    const htmlRow = `
    <tr>
      <td>${product.name}</td>
      <td>${product.price}</td>
      <td>
        <div class="qty mt-0">
                        <span class="minus bg-dark" onclick="qtyDecrement(${
                          product.id
                        })">-</span>
                        <input type="number" class="count" name="qty" value=${
                          product.productQty
                        } min=0>
                        <span class="plus bg-dark" onclick="qtyIncrement(${
                          product.id
                        })">+</span>
        </div>
      </td>
      <td>${product.price * product.productQty}</td>
      <td>
      <button type="button" class="btn btn-danger" onclick="removeFromCart(${
        product.id
      })">Remove</button>
      </td>
      <td> Coming Soon </td>
    </tr>
    `;
    htmlRows += htmlRow;
  });

  $("#tcartbody").html(htmlRows);
}

function removeFromCart(id) {
  let lst = getCartProducts();
  const productsInCart = lst.filter((x) => x.id == id);
  let productInCart = productsInCart[0];

  lst = lst.filter((x) => x.id != id);
  const jsonCart = JSON.stringify(lst);
  localStorage.setItem(tblProductCart, jsonCart);

  successMessage("Remove from cart successful");
  getCartTable();
}

function qtyIncrement(id) {
  let lst = getCartProducts();
  const productsInCart = lst.filter((x) => x.id == id);
  let productInCart = productsInCart[0];
  productInCart.productQty += 1;

  const jsonCartProduct = JSON.stringify(lst);
  localStorage.setItem(tblProductCart, jsonCartProduct);

  // successMessage("Adding to cart succesful");

  getCartTable();
}

function qtyDecrement(id) {
  let lst = getCartProducts();
  const productsInCart = lst.filter((x) => x.id == id);
  let productInCart = productsInCart[0];
  productInCart.productQty -= 1;

  if (productInCart.productQty <= 0) {
    removeFromCart(id);
    return;
  }

  const jsonCartProduct = JSON.stringify(lst);
  localStorage.setItem(tblProductCart, jsonCartProduct);

  // successMessage("Removing from cart succesful");

  getCartTable();
}

///////////////////////////////////

function successMessage(message) {
  // alert(message);

  Swal.fire({
    title: "Action Success",
    text: message,
    icon: "success",
  });
}

function errorMessage(message) {
  Swal.fire({
    title: "Action Failed",
    text: message,
    icon: "error",
  });
}
