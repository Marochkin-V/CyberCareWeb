const accountLink = document.querySelector('a[href="/Home/Auth"]');
const accountLinkText = localStorage.getItem('userName');

// Проверяем, что ссылка найдена
if (accountLink && accountLinkText) {
    accountLink.textContent = accountLinkText;
}
else {
    accountLink.textContent = "Аккаунт";
}

const usersLink = document.querySelector('a[href="/Tables/Users"]');
const role = localStorage.getItem('role');
if (role !== "Admin") {
    usersLink.style.display = 'none';
}