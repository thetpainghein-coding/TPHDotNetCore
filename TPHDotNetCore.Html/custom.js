function successMessage(message) {
  Swal.fire({
    title: "Action Success",
    text: message,
    icon: "success",
  });
  // Notiflix.Report.success(
  //   "Notiflix Success",
  //   '"Do not try to become a person of success but try to become a person of value." <br/><br/>- Albert Einstein',
  //   "Okay"
  // );
}

function errorMessage(message) {
  Swal.fire({
    title: "Action Success",
    text: message,
    icon: "error",
  });

  // Notiflix.Report.failure(
  //   "Notiflix Success",
  //   '"Do not try to become a person of success but try to become a person of value." <br/><br/>- Albert Einstein',
  //   "Okay"
  // );
}

function uuidv4() {
  return "10000000-1000-4000-8000-100000000000".replace(/[018]/g, (c) =>
    (
      +c ^
      (crypto.getRandomValues(new Uint8Array(1))[0] & (15 >> (+c / 4)))
    ).toString(16)
  );
}

function ConfirmMessage() {
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
