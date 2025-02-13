const tblBlog = "blogs";
let blogId = null;

getBlogTable();
// testConfirmMessage2();

function testConfirmMessage() {
  let confirmMessage = new Promise(function (success, error) {
    const result = confirm("Are you sure want to delete?");
    if (result) {
      success();
    } else {
      error();
    }
  });

  confirmMessage.then(
    function (value) {
      successMessage("Success");
    },
    function (error) {
      errorMessage("Error");
    }
  );
}

function testConfirmMessage2() {
  let confirmMessage = new Promise(function (success, error) {
    Swal.fire({
      title: "Confirm",
      text: "Are you sure want to delete?",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Yes",
    }).then((result) => {
      if (result.isConfirmed) {
        success();
      } else {
        error();
      }
    });
  });

  confirmMessage.then(
    function (value) {
      successMessage("Success");
    },
    function (error) {
      errorMessage("Error");
    }
  );
}
// createBlog()
// updateBlog("2cc03e03-4e0c-404b-a4fb-4efaa923b38b", "new data","new data","new data")
deleteBlog(
  "2cc03e03-4e0c-404b-a4fb-4efaa923b38b",
  "new data",
  "new data",
  "new data"
);

function readBlog() {
  let lst = getBlogs();
  console.log(lst);
}

function createBlog(title, author, content) {
  let lst = getBlogs();

  const requestModel = {
    id: uuidv4(),
    title: title,
    author: author,
    content: content,
  };

  lst.push(requestModel);

  const jsonBlog = JSON.stringify(lst);
  localStorage.setItem(tblBlog, jsonBlog);

  successMessage("Saving Succesful");

  clearControl();
}

function editBlog(id) {
  let lst = getBlogs();

  const items = lst.filter((x) => x.id === id);
  // console.log(items);
  console.log(items.length);
  if (items.length == 0) {
    console.log("No Data Found");
    errorMessage("No Data Found");
    return;
  }

  let item = items[0];

  blogId = item.id;
  $("#txtTitle").val(item.title);
  $("#txtAuthor").val(item.author);
  $("#txtContent").val(item.content);
}

function updateBlog(id, title, author, content) {
  let lst = getBlogs();

  const items = lst.filter((x) => x.id === id);
  // console.log(items);
  console.log(items.length);
  if (items.length == 0) {
    console.log("No Data Found");
    errorMessage("No Data Found");
    return;
  }

  const item = items[0];
  console.log(item);

  item.title = title;
  item.author = author;
  item.content = content;

  const index = lst.findIndex((x) => x.id === id);
  console.log(index);
  lst[index] = item;

  const jsonBlog = JSON.stringify(lst);
  localStorage.setItem(tblBlog, jsonBlog);

  successMessage("Updating Succesful");

  console.log(lst[index]);
}

function deleteBlog(id) {
  Swal.fire({
    title: "Confirm",
    text: "Are you sure want to delete?",
    icon: "warning",
    showCancelButton: true,
    confirmButtonText: "Yes",
  }).then((result) => {
    if (!result.isConfirmed) return;
    let lst = getBlogs();

    const items = lst.filter((x) => x.id === id);
    if (items.length == 0) {
      console.log("No Data Found");
      return;
    }

    // console.log(items.length);
    // if(items.length == 0){
    //   console.log("No Data Found");
    //   return;
    // }

    lst = lst.filter((x) => x.id !== id);
    const jsonBlog = JSON.stringify(lst);
    localStorage.setItem(tblBlog, jsonBlog);

    successMessage("Deleteing Succesful");
    getBlogTable();

    console.log(tblBlog.length);
    console.log(tblBlog);
  });
}

function getBlogs() {
  const blogs = localStorage.getItem(tblBlog);
  console.log(blogs);

  let lst = [];
  if (blogs !== null) {
    lst = JSON.parse(blogs);
  }

  return lst;
}

$("#btnSave").click(function () {
  const title = $("#txtTitle").val();
  const author = $("#txtAuthor").val();
  const content = $("#txtContent").val();

  if (blogId === null) {
    createBlog(title, author, content);
  } else {
    updateBlog(blogId, title, author, content);
    clearControl();
    blogId = null;
  }

  getBlogTable();
});

$("#btnCancel").click(function () {
  clearControl();
});

function clearControl() {
  $("#txtTitle").val("");
  $("#txtAuthor").val("");
  $("#txtContent").val("");

  $("#txtTitle").focus();
}

function getBlogTable() {
  const lst = getBlogs();
  let count = 0;
  let htmlRows = "";
  lst.forEach((item) => {
    const htmlRow = `
    <tr>
      <td>
        <button type="button" class="btn btn-warning" onclick="editBlog('${
          item.id
        }')">Edit</button>
        <button type="button" class="btn btn-danger" onclick="deleteBlog('${
          item.id
        }')" >Delete</button>
      </td>
      <td>${++count}</td>
      <td>${item.title}</td>
      <td>${item.author}</td>
      <td>${item.content}</td>
    </tr>
    `;
    htmlRows += htmlRow;
  });

  $("#tbody").html(htmlRows);
}
