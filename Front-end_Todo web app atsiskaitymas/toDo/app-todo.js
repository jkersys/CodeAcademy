//Tikrinam ar prisijungęs
let isConected = () => {
    KeyName = Object.getOwnPropertyNames(localStorage)
    if (KeyName.length < 1) {
        window.location.href = '../index.html'
    }
}
//nuskaitom raktą
let KeyName = Object.getOwnPropertyNames(localStorage)
setInterval(isConected, 1000)

const url = 'https://testapi.io/api/jkersys/resource/userPosts/'
const postForm = document.querySelector('#todo-form');
const showUserPosts = document.querySelector('#print-user-posts');
const btnToggleTodo = document.querySelector('#addTodo')
const userTypeInput = document.querySelector('#type-input')
const userTypeContent = document.querySelector('#content-input')
const userTypeDate = document.querySelector('#date-input')
const userTypeInputError = document.querySelector('#fieldTypeError')
const userTypeContentError = document.querySelector('#fieldContentError')
const userTypeDateError = document.querySelector('#fieldDateError')
let UserKey = JSON.parse(localStorage.getItem(KeyName))
//console.log(UserKey)
let activeUser = UserKey.firstname + ' ' + UserKey.lastname
//console.log(activeUser)
document.querySelector('#user-name').innerHTML = activeUser
let inputForm = document.querySelector('#todo-form')

//NEW POST
const sendPostData = () => {
    let data = new FormData(postForm);
    let obj = {};
    obj[`username`] = activeUser
    data.forEach((value, key) => {
        obj[key] = value;
    });
    //console.log(obj)
    fetch(url, {
        method: "post",
        headers: {
            Accept: "application/json, text/plain, */*",
            "Content-Type": "application/json",
        },
        body: JSON.stringify(obj),
    })
        .then(() => printUserPosts())
        .catch((error) => console.log(error));
    //console.log(data);
}
btn_post.addEventListener("click", (e) => {
    e.preventDefault();
    inputFieldsValidation()

});
//INPUT VALIDATOR
const inputFieldsValidation = () => {
    let typeValidation = true
    let contentValidation = true
    let dateValidation = true
    if (userTypeInput.value.length === 0) {
        userTypeInputError.style.visibility = "visible";
        typeValidation = false
    }
    else {
        userTypeInputError.style.visibility = 'hidden'
    }
    if (userTypeContent.value.length === 0) {
        userTypeContentError.style.visibility = "visible";
        contentValidation = false
    }
    else {
        userTypeContentError.style.visibility = 'hidden'
    }
    if (userTypeDate.value === '') {
        userTypeDateError.style.visibility = 'visible'
        dateValidation = false
    }
    else {
        userTypeDateError.style.visibility = "hidden";
    }

    if (!typeValidation || !contentValidation || !dateValidation) {
        return
    }
    else {
        sendPostData();
    }
}
//PRINT POSTS
const printUserPosts = () => {
    const options = {
        method: 'get',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    }
    fetch(url, options)
        .then((response) => response.json())
        .then((posts) => {
            const userPosts = posts.data.filter(x => x.username === activeUser);
            showUserPosts.innerHTML = '';
            let headers = `<div class="printedDataContainer">
                <div class="printedDataContainer"><div class="postType">TYPE</div>` +
                `<div class="postContent">CONTENT</div>` +
                `<div class="postEndDate">END DATE</div>` +
                `<div class="button_container">COMMANDS</div></div>`;
            showUserPosts.innerHTML = headers;
            for (const post of userPosts) {
                let activeUserPost = `<div id="${post.id}" class="printedDataContainer">
                 <div class="postType">${post.type}</div><input style="display:none" type="text" class='postTypeInput' />` +
                    `<div class="postContent">${post.content}</div><input style="display:none" type="text" class='postContentInput' />` +
                    `<div class="postEndDate">${post.endDate}</div><input style="display:none" type="date" class='postEndDateInput' />` +
                    '<div class="button_container"><input type="button" class="editButton" value="Edit" onClick="editTodo(' + post.id + ')" />' +
                    '<input type="button" class="updateButton" style="display:none" value="Update" onClick="updateTodo(' + post.id + ')" />' +
                    '<input type="button" class="cancelButton" style="display:none" value="Cancel" onClick="cancelTodo(' + post.id + ')" />' +
                    '<input type="button" class="yesButton" style="display:none" value="YES" onClick="deleteTodo(' + post.id + ')" />' +
                    '<input type="button" class="noButton" style="display:none" value="NO" onClick="cancelDelete(' + post.id + ')" />' +
                    //'<input type="button" value="Delete" onClick="deleteTodo(' + post.id + ')" /> </div> </div> '
                    '<input type="button" class="deleteButton" value="Delete" onClick="aproveDelete(' + post.id + ')" /> </div> </div> '
                showUserPosts.innerHTML += activeUserPost
            }
        });
}

printUserPosts()
//DELETE POST
const deleteForm = document.querySelector('#delete-todo');
const deleteTodoBtn = document.querySelector('#btn_delete');


const updateTodo = (id) => {
    const div = document.getElementById(id);
    const typeValue = div.getElementsByClassName('postTypeInput')[0].value;
    const contentValue = div.getElementsByClassName('postContentInput')[0].value;
    const endDateValue = div.getElementsByClassName('postEndDateInput')[0].value;

    const post = {
        username: activeUser,
        content: contentValue,
        endDate: endDateValue,
        type: typeValue
    }

    const optionsFetchPosts = {
        method: 'put',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(post)
    }

    fetch(url + id, optionsFetchPosts)
        .then(() => printUserPosts())
        .catch((error) => {
            console.log(`Request failed with error: ${error}`);
        })
}
const editTodo = (id) => {

    //type update

    const div = document.getElementById(id);
    const typeDiv = div.getElementsByClassName('postType')[0];
    const typeInput = div.getElementsByClassName('postTypeInput')[0];
    const editButton = div.getElementsByClassName('editButton')[0];
    const cancelButton = div.getElementsByClassName('cancelButton')[0];
    const updateButton = div.getElementsByClassName('updateButton')[0];
    const deleteButton = div.getElementsByClassName('deleteButton')[0]
    deleteButton.style.display = "none";
    editButton.style.display = "none";
    updateButton.style.display = "block";
    cancelButton.style.display = 'block';
    typeDiv.style.display = "none";
    typeInput.style.display = "block";
    typeInput.value = typeDiv.innerHTML;

    //content update
    const contentDiv = div.getElementsByClassName('postContent')[0];
    const contentInput = div.getElementsByClassName('postContentInput')[0];
    contentDiv.style.display = "none";
    contentInput.style.display = "block";
    contentInput.value = contentDiv.innerHTML;

    //endDate update
    const endDateDiv = div.getElementsByClassName('postEndDate')[0];
    const endDateInput = div.getElementsByClassName('postEndDateInput')[0];
    endDateDiv.style.display = "none";
    endDateInput.style.display = "block";
    endDateInput.value = endDateDiv.innerHTML;
}

let aproveDelete = (id) => {
    const div = document.getElementById(id);
    const editButton = div.getElementsByClassName('editButton')[0];
    const deleteButton = div.getElementsByClassName('deleteButton')[0];
    const yesButton = div.getElementsByClassName('yesButton')[0];
    const noButton = div.getElementsByClassName('noButton')[0];
    editButton.style.display = "none";
    deleteButton.style.display = "none";
    yesButton.style.display = "block";
    noButton.style.display = "block";
}
let cancelDelete = (id) => {
    const div = document.getElementById(id);
    const editButton = div.getElementsByClassName('editButton')[0];
    const deleteButton = div.getElementsByClassName('deleteButton')[0];
    const yesButton = div.getElementsByClassName('yesButton')[0];
    const noButton = div.getElementsByClassName('noButton')[0];
    editButton.style.display = "block";
    deleteButton.style.display = "block";
    yesButton.style.display = "none";
    noButton.style.display = "none";
}

let cancelTodo = (id) => {
    const div = document.getElementById(id);
    const typeInput = div.getElementsByClassName('postTypeInput')[0];
    const typeDiv = div.getElementsByClassName('postType')[0];

    const cancelButton = div.getElementsByClassName('cancelButton')[0];
    const updateButton = div.getElementsByClassName('updateButton')[0];
    const editButton = div.getElementsByClassName('editButton')[0];

    const contentInput = div.getElementsByClassName('postContentInput')[0];
    const contentDiv = div.getElementsByClassName('postContent')[0];

    const endDateDiv = div.getElementsByClassName('postEndDate')[0];
    const endDateInput = div.getElementsByClassName('postEndDateInput')[0];
    const deleteButton = div.getElementsByClassName('deleteButton')[0]
    deleteButton.style.display = "block";
    typeInput.style.display = "none";
    typeDiv.style.display = "block";
    cancelButton.style.display = "none";
    updateButton.style.display = "none";
    editButton.style.display = "block";
    contentInput.style.display = "none";
    contentDiv.style.display = "block";
    endDateInput.style.display = "none";
    endDateDiv.style.display = "block";
}

const deleteTodo = (id) => {
    const optionsFetchPosts = {
        method: 'delete',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    }
    fetch(url + id, optionsFetchPosts)
        .then(() => printUserPosts())
        .catch((error) => {
            console.log(`Request failed with error: ${error}`);
        })
}

btn_logout.onclick = () => {
    localStorage.clear();
    window.location.href = '../index.html'
};

btnToggleTodo.onclick = () => {
    toggleTodo()
}

let toggleTodo = () => {


    if (inputForm.style.display === 'none' || inputForm.style.display === '') {
        inputForm.style.display = 'grid';
    } else {
        inputForm.style.display = 'none';
    }
}