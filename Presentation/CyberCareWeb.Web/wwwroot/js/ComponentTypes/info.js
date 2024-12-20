function info(detailsButton) {
    const row = detailsButton.closest('tr');
    const id = row.dataset.id;

    const modal = document.getElementById("modal");
    const modalContent = modal.querySelector(".modal-info-content");

    // Заполнение модального окна данными
    modalContent.innerHTML = `
        <h3>Детальная информация о типе компонента</h3>
        <p><strong>Название:</strong> ${row.cells[0].innerText}</p>
        <p><strong>Описание:</strong> ${row.cells[1].innerText}</p>
        <button onclick="closeModal()">Назад</button>
        <button class="delete-button" onclick="deleteRow('${id}')">Удалить</button>
    `;

    // Отображение модального окна
    modal.style.display = "block";
}

function closeModal() {
    const modal = document.getElementById("modal");
    modal.style.display = "none";
}

async function deleteRow(id) {
    if (confirm("Вы уверены, что хотите удалить этот тип компонента?")) {
        try {
            await axios.delete(`${apiBaseUrl}/${id}`);
            alert("Тип компонента успешно удалён.");
            location.reload(); // Обновляем страницу после удаления
        } catch (error) {
            console.error("Ошибка при удалении типа компонента:", error);
            alert("Не удалось удалить запись. Повторите попытку позже.");
        }
    }
}
