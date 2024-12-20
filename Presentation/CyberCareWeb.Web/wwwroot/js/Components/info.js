function info(viewButton) {
    const row = viewButton.closest("tr");
    const id = row.dataset.id;

    const modal = document.getElementById("modal");
    const modalContent = modal.querySelector(".modal-info-content");

    modalContent.innerHTML = `
        <h3>Детальная информация о компоненте</h3>
        <p><strong>Тип компонента:</strong> ${row.cells[0].innerText}</p>
        <p><strong>Бренд:</strong> ${row.cells[1].innerText}</p>
        <p><strong>Производитель:</strong> ${row.cells[2].innerText}</p>
        <p><strong>Спецификации:</strong> ${row.cells[3].innerText}</p>
        <p><strong>Срок гарантии:</strong> ${row.cells[4].innerText} месяцев</p>
        <p><strong>Цена:</strong> ${row.cells[5].innerText} ₽</p>
        <button onclick="closeModal()">Назад</button>
        <button class="delete-button" onclick="deleteRow('${id}')">Удалить</button>
    `;

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