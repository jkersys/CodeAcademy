const loginFormSbmBtn = document.querySelector('#login-submit');
const nameErrorMsg = document.querySelector('#fieldNameError')
const lastnameErrorMsg = document.querySelector('#fieldLastnameError')

const inputFirstName = document.querySelector('#input-firstname');
const inputLastName = document.querySelector('#input-lastname');
const failMsg = document.querySelector('#redTable');
const url = 'https://testapi.io/api/jkersys/resource/users';
const options = {
    method: 'get',
    headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
    }
}

const response = {};

function userValidation() {

    fetch(url, options)
        .then((response) => response.json())
        .then((users) => {
            //console.log(users);
            let loggedUser = null;
            for (const user of users.data) {
                if (user.firstname.toLowerCase() === inputFirstName.value.toLowerCase().trim() &&
                    user.lastname.toLowerCase() === inputLastName.value.toLowerCase().trim()) {
                    loggedUser = user;
                    break;
                }
            };
            if (loggedUser != null) {
                const userData = JSON.stringify(loggedUser)
                localStorage.setItem(loggedUser.firstname + ' ' + loggedUser.lastname, userData)
                failMsg.innerHTML = (`${loggedUser.firstname} ${loggedUser.lastname}`)
                window.location.href = '../todo/todo.html'
            }
            else {
                failMsg.style.visibility = "visible"
                if (inputFirstName.value.length < 0) {
                    errorMsg.style.display = 'block'
                }
            }
        })
}

let fieldsValidation = () => {
    failMsg.style.visibility = "hidden"
    if (inputFirstName.value.length === 0) {
        nameErrorMsg.style.visibility = "visible";
    }
    else {
        nameErrorMsg.style.visibility = 'hidden'
    }
    if (inputLastName.value.length === 0) {
        lastnameErrorMsg.style.visibility = "visible";
    }
    else {
        lastnameErrorMsg.style.visibility = "hidden";
    }
    if (inputFirstName.value.length === 0 || inputLastName.value.length === 0) {
        return
    }
    userValidation();

}

btn_return.onclick = () => {
    localStorage.clear();
    window.location.href = "../index.html"
};

loginFormSbmBtn.addEventListener("click", (e) => {
    e.preventDefault();
    localStorage.clear();
    fieldsValidation()
});

