const reg = document.getElementById("register-form");
const log = document.getElementById("login-form");
const regBtm = document.getElementById("reg-Form");
const logBtm = document.getElementById("log-Form");
function showlogin() {

    reg.style.display = "none";

    log.style.display = "block";

    regBtm.classList.remove("active")
    logBtm.classList.add("active")

};

function showRegister() {

    reg.style.display = "block";

    log.style.display = "none";


    logBtm.classList.remove("active")
    regBtm.classList.add("active")

};

function closeform() {

    const container = document.getElementById("section__user");
    container.style.right = '-1000px';

}
function SHowLoginAndRegester() {
    const container = document.getElementById("section__user");
    container.style.right = '0';
    // container.style.display = 'block';

}