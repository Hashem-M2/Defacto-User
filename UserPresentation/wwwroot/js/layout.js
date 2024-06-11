
    let femaleButton = document.getElementById('femaleButton');
    let maleButton = document.getElementById('maleButton');
    let kidsButton = document.getElementById('kidsButton');

function femalebtn() {
    femaleButton.style.backgroundColor = 'Black';
    maleButton.style.backgroundColor = 'white';
    kidsButton.style.backgroundColor = 'white';
    femaleButton.style.color = 'white';
    maleButton.style.color = 'black';
    kidsButton.style.color = 'black';
}
      
   
    function malebtn() {
        maleButton.style.backgroundColor = 'Black';
        femaleButton.style.backgroundColor = 'white';
        kidsButton.style.backgroundColor = 'white';
    maleButton.style.color = 'white';
    femaleButton.style.color = 'black';
    kidsButton.style.color = 'black';
        }
    function kids() {
        kidsButton.style.backgroundColor = 'Black';
        femaleButton.style.backgroundColor = 'white';
        maleButton.style.backgroundColor = 'white';
    kidsButton.style.color = 'white';
    femaleButton.style.color = 'black';
    maleButton.style.color = 'black';
        }

function changeLanguage() {
    var selectedLanguage = $("#SelectedLanguage").val();
    window.location.href = '/man/Index?culture=' + selectedLanguage;
}



// login and register templeate 
/*
var logbtning = document.getElementById("logid");
    var registerbtn = document.getElementById("registerid");
    var logform = document.getElementById("login-form");
    var registerformin = document.getElementById("registrationForm");
    registerformin.style.display = "none";

    registerbtn.addEventListener('click', function () {
        logform.style.display = "none";
    registerformin.style.display = "block";

});

    logbtning.addEventListener('click', function () {
        logform.style.display = "block";
    registerformin.style.display = "none";
});

    // Close window
    var closePageBtn = document.getElementById("closePageBtn");
    var closepage = document.getElementById("loginingregestering");
    var logbtnid = document.getElementById("loginBtn");

    closePageBtn.addEventListener('click', function () {
        closepage.style.display = "none";
});

    logbtnid.addEventListener('click', function () {
        closepage.style.display = "block";
});

    // Show login form
    var loginForm = document.getElementById('loginForm');
    var btn = document.getElementById("loginBtn");

    btn.addEventListener('click', function () {
        loginForm.classList.add('show');
});


    const contactMethodRadios = document.querySelectorAll('input[name="contact-method"]');
    const emailInput = document.getElementById('email');
  //  const phoneInput = document.getElementById('phone-input');
    const emailLabel = document.querySelector('label[for="email"]');
    const phoneLabel = document.querySelector('label[for="phone"]');

    // Function to toggle the visibility of the input fields and their labels
    function toggleInputVisibility() {
    const selectedMethod = document.querySelector('input[name="contact-method"]:checked').value;
    emailInput.style.display = selectedMethod === 'email' ? 'block' : 'none';
   // phoneInput.style.display = selectedMethod === 'phone' ? 'block' : 'none';
    emailLabel.style.display = selectedMethod === 'email' ? 'block' : 'none';
    phoneLabel.style.display = selectedMethod === 'phone' ? 'block' : 'none';
}

// Add event listeners to the radio buttons
contactMethodRadios.forEach(radio => {
        radio.addEventListener('change', toggleInputVisibility);
});

    toggleInputVisibility();
    */

    function changeLanguage() {
    var selectedLanguage = $("#SelectedLanguage").val();
    window.location.href = '/man/Index?culture=' + selectedLanguage;
}




