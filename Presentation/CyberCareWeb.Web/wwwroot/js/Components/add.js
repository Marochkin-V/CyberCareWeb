function addEmptyRow() {
    const table = document.querySelector("#table-container table tbody");

    // Создаём новую строку
    const newRow = document.createElement("tr");
    newRow.dataset.id = "new"; // Временный ID для новой строки

    newRow.innerHTML = `
        <td data-field="componentType" data-componentType-id ="0"></td>
        <td style="padding: 8px;" contenteditable="true"></td>
        <td style="padding: 8px;" contenteditable="true"></td>
        <td style="padding: 8px;" contenteditable="true"></td>
        <td style="padding: 8px;" contenteditable="true"></td>
        <td style="padding: 8px;" contenteditable="true"></td>
        <td style="padding: 8px;">
            <a href="javascript:void(0);" onclick="saveNewRow(this)" title="Save">
                <i class="bi bi-check-circle-fill"></i>
            </a>
            <a href="javascript:void(0);" onclick="cancelNewRow(this)" title="Cancel">
                <i class="bi bi-x-circle-fill"></i>
            </a>
        </td>
    `;

    const cells = Array.from(newRow.querySelectorAll('td')).filter(cell => !cell.classList.contains('actions'));
    newRow.classList.add('editing');
    newRow.dataset.originalData = JSON.stringify(cells.map(cell => cell.innerText.trim()));

    cells.forEach(cell => {
        if (cell.dataset.field === "componentType") {
            cell.addEventListener('click', () => openSelectModal(cell));
        }
    });

    cells.forEach(cell => cell.setAttribute('contenteditable', 'true')); // Только данные можно редактировать


    // Вставляем новую строку в начало таблицы
    table.prepend(newRow);
}

async function saveNewRow(saveButton) {
    const row = saveButton.closest("tr");
    const cells = Array.from(row.querySelectorAll('td')).filter(cell => !cell.classList.contains('actions'));

    // Собираем данные из строки
    const newItem = {
        componentTypeId: cells[0].dataset.componentTypeId, // Хранится ID типа компонента
        brand: cells[1].innerText.trim(),
        manufactorer: cells[2].innerText.trim(),
        specifications: cells[3].innerText.trim(),
        warrantyPeriod: parseInt(cells[4].innerText.trim()),
        price: parseFloat(cells[5].innerText.trim())
    };

    // Проверяем заполненность полей
    if (!newItem.brand || !newItem.manufactorer || !newItem.specifications || isNaN(newItem.warrantyPeriod) || isNaN(newItem.price)) {
        alert("Не все поля заполнены корректно.");
        return;
    }

    try {
        // Отправляем данные на сервер
        const response = await axios.post(apiBaseUrl, newItem);

        if (response.status === 201) {
            alert("Данные созданые успешно!");
            location.reload();
        }
        else {
            throw new Error("Ошбика создания данных");
        }
    } catch (error) {
        console.error("Ошбика создание данных:", error);
        alert("Ошбика при создании данных. Потворите попытку позже");

        // Удаляем строку при ошибке
        row.remove();
    }
}

function cancelNewRow(cancelButton) {
    const row = cancelButton.closest("tr");
    row.remove(); // Удаляем строку
}