function addEmptyRow() {
    const table = document.querySelector("#table-container table tbody");

    // Создаём новую строку
    const newRow = document.createElement("tr");
    newRow.dataset.id = "new"; // Временный ID для новой строки

    newRow.innerHTML = `
        <td style="padding: 8px;" contenteditable="true" data-field="name" placeholder="Введите название"></td>
        <td style="padding: 8px;" contenteditable="true" data-field="description" placeholder="Введите описание"></td>
        <td style="padding: 8px;">
            <a href="javascript:void(0);" onclick="saveNewRow(this)" title="Сохранить">
                <i class="bi bi-check-circle-fill"></i>
            </a>
            <a href="javascript:void(0);" onclick="cancelNewRow(this)" title="Отменить">
                <i class="bi bi-x-circle-fill"></i>
            </a>
        </td>
    `;

    // Вставляем новую строку в начало таблицы
    table.prepend(newRow);
}

async function saveNewRow(saveButton) {
    const row = saveButton.closest("tr");
    const cells = Array.from(row.querySelectorAll("td[contenteditable]"));

    // Собираем данные из строки
    const newItem = {
        name: cells.find(cell => cell.dataset.field === "name")?.innerText.trim(),
        description: cells.find(cell => cell.dataset.field === "description")?.innerText.trim(),
    };

    // Проверяем заполненность полей
    if (!newItem.name || !newItem.description) {
        alert("Не все поля заполнены.");
        return;
    }

    try {
        // Отправляем данные на сервер
        const response = await axios.post(apiBaseUrl, newItem);

        if (response.status === 201) {
            alert("Данные созданы успешно!");

            // Обновляем строку с новыми данными
            row.dataset.id = response.data.id; // Устанавливаем ID, полученный от сервера
            row.innerHTML = `
                <td style="padding: 8px;" contenteditable="false">${response.data.name}</td>
                <td style="padding: 8px;" contenteditable="false">${response.data.description}</td>
                <td style="padding: 8px;">
                    <a href="javascript:void(0);" onclick="editRow(this)" title="Редактировать">
                        <i class="bi bi-pencil-fill"></i>
                    </a>
                    <a href="javascript:void(0);" onclick="info(this)" title="Просмотр">
                        <i class="bi bi-eye-fill"></i>
                    </a>
                </td>
            `;
        } else {
            throw new Error("Ошибка создания записи.");
        }
    } catch (error) {
        console.error("Ошибка создания записи:", error);
        alert("Ошибка при создании записи. Повторите попытку позже.");

        // Удаляем строку при ошибке
        row.remove();
    }
}

function cancelNewRow(cancelButton) {
    const row = cancelButton.closest("tr");
    row.remove(); // Удаляем строку
}
