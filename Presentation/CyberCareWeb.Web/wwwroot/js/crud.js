function createTable(itemsLength, totalItems, currentPage, tableTitle, tableHead, tableBody, loadDataFunction = loadData) {
    const container = document.getElementById("table-container");
    container.innerHTML = "";

    if (itemsLength === 0) {
        container.innerHTML = `Данные в таблице отсутствуют.`;
        return;
    }

    const table = document.createElement("table");
    const caption = document.createElement("caption");
    caption.innerHTML = `
        ${tableTitle}
        <a class="edit-buttons" href="javascript:void(0);" onclick="addEmptyRow()" title="Add Item">
            <i class="bi bi-plus-square-fill"></i>
        </a>
    `;
    table.appendChild(caption);

    const thead = document.createElement("thead");
    thead.innerHTML = tableHead;
    table.appendChild(thead);

    const tbody = document.createElement("tbody");
    tbody.innerHTML = tableBody;
    table.appendChild(tbody);

    container.appendChild(table);

    createPagination(totalItems, currentPage, loadDataFunction);
}

function createPagination(totalItems, currentPage, loadDataFunction = loadData) {
    const container = document.getElementById("table-container");
    const totalPages = Math.ceil(totalItems / itemsPerPage);

    const paginationDiv = document.createElement("div");
    paginationDiv.style.textAlign = "center";
    paginationDiv.style.marginTop = "10px";

    // Кнопка для перехода на предыдущую страницу
    const prevButton = document.createElement("button");
    prevButton.innerText = "←";
    prevButton.style.margin = "0 5px";
    prevButton.style.padding = "5px 10px";
    prevButton.style.background = currentPage > 1 ? "#3498DB" : "#ccc";
    prevButton.style.color = currentPage > 1 ? "#fff" : "#666";
    prevButton.style.border = "1px solid #ccc";
    prevButton.style.borderRadius = "4px";
    prevButton.style.cursor = currentPage > 1 ? "pointer" : "not-allowed";
    prevButton.onclick = () => {
        if (currentPage > 1) loadDataFunction(currentPage - 1);
    };

    paginationDiv.appendChild(prevButton);

    // Текущая страница и поле для ввода номера страницы
    const currentPageText = document.createElement("span");
    currentPageText.innerText = `Page ${currentPage} of ${totalPages}`;
    currentPageText.style.margin = "0 10px";
    paginationDiv.appendChild(currentPageText);

    const pageInput = document.createElement("input");
    pageInput.type = "number";
    pageInput.min = 1;
    pageInput.max = totalPages;
    pageInput.value = currentPage;
    pageInput.style.width = "50px";
    pageInput.style.margin = "0 5px";
    pageInput.onchange = () => {
        const inputPage = parseInt(pageInput.value, 10);
        if (!isNaN(inputPage)) {
            if (inputPage < 1) loadDataFunction(1);
            else if (inputPage > totalPages) loadDataFunction(totalPages);
            else loadDataFunction(inputPage);
        }
    };
    paginationDiv.appendChild(pageInput);

    // Кнопка для перехода на следующую страницу
    const nextButton = document.createElement("button");
    nextButton.innerText = "→";
    nextButton.style.margin = "0 5px";
    nextButton.style.padding = "5px 10px";
    nextButton.style.background = currentPage < totalPages ? "#3498DB" : "#ccc";
    nextButton.style.color = currentPage < totalPages ? "#fff" : "#666";
    nextButton.style.border = "1px solid #ccc";
    nextButton.style.borderRadius = "4px";
    nextButton.style.cursor = currentPage < totalPages ? "pointer" : "not-allowed";
    nextButton.onclick = () => {
        if (currentPage < totalPages) loadDataFunction(currentPage + 1);
    };

    paginationDiv.appendChild(nextButton);
    container.appendChild(paginationDiv);
}
function applyFilters() {
    currentPage = 1; // Сбрасываем на первую страницу
    loadData(currentPage);
}

async function deleteRow(id) {
    try {
        const response = await axios.delete(`${apiBaseUrl}/${id}`);
        if (response.status === 204) {
            const row = document.querySelector(`tr[data-id="${id}"]`);
            if (row) row.remove(); // Удаляем строку из таблицы
            alert("Запись была успешно удалена.");
        } else {
            alert("Не удалось найти запись.");
        }
    } catch (error) {
        console.error("Ошибка при удалении:", error);
        if (error.response) {
            // Ответ от сервера с ошибкой
            alert(`Ошибка сервера: ${error.response.status} - ${error.response.statusText}`);
        } else {
            // Ошибка запроса (например, нет соединения)
            alert("Сетевая ошибка: Не удалось удалить запись.");
        }
    }

    // Закрываем модальное окно после удаления
    closeModal();
}

function closeModal() {
    const modal = document.getElementById("modal");
    modal.style.display = "none";
}

function ERROR(error) {
    console.error("Error fetching data:", error);
    document.getElementById("table-container").innerHTML =
        `<p>Error loading data. Please try again later.</p>`;
}
function cancelEditing(row) {
    const cells = row.querySelectorAll('td[contenteditable]');
    const originalData = JSON.parse(row.dataset.originalData);

    // Возвращаем исходные значения
    cells.forEach((cell, index) => {
        cell.innerText = originalData[index];
        cell.setAttribute('contenteditable', 'false');
    });

    row.classList.remove('editing');

    // Убираем кнопки сохранения и отмены
    const editButton = row.querySelector('a[title="Save"]');
    if (editButton) {
        editButton.innerHTML = '<i class="bi bi-pencil-fill"></i>'; // Иконка редактирования
        editButton.title = "Edit";
    }

    const cancelButton = row.querySelector('.cancel-button');
    if (cancelButton) cancelButton.remove();
}