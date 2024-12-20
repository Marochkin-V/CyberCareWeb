function editRow(editButton) {
    const row = editButton.closest('tr');
    const cells = Array.from(row.querySelectorAll('td')).filter(cell => !cell.classList.contains('actions')); // Исключаем столбец действий
    const isEditing = row.classList.contains('editing');

    if (isEditing) {
        // Сохранение изменений
        const id = row.dataset.id;
        const updatedData = {
            id: id,
            componentTypeId: cells[0].dataset.componentTypeId, // Хранится ID типа компонента
            brand: cells[1].innerText.trim(),
            manufactorer: cells[2].innerText.trim(),
            specifications: cells[3].innerText.trim(),
            warrantyPeriod: parseInt(cells[4].innerText.trim()),
            price: parseFloat(cells[5].innerText.trim())
        };

        saveChanges(id, updatedData, row);
    } else {
        // Начало редактирования
        row.classList.add('editing');
        row.dataset.originalData = JSON.stringify(cells.map(cell => cell.innerText.trim()));

        cells.forEach(cell => {
            if (cell.dataset.field === "componentType") {
                cell.addEventListener('click', () => openSelectModal(cell));
            }
        });
        cells.forEach(cell => cell.setAttribute('contenteditable', 'true')); // Только данные можно редактировать

        editButton.innerHTML = '<i class="bi bi-check-circle-fill"></i>'; // Иконка сохранения
        editButton.title = "Save";

        // Добавляем кнопку отмены
        const cancelButton = document.createElement('a');
        cancelButton.innerHTML = '<i class="bi bi-x-circle-fill"></i>'; // Иконка крестика
        cancelButton.title = "Cancel";
        cancelButton.className = "cancel-button";
        cancelButton.onclick = () => cancelEditingComponenttype(row);
        row.querySelector('td.actions').appendChild(cancelButton); // Кнопка отмены только в actions
    }
}

async function saveChanges(id, updatedData, row) {
    try {
        await axios.put(`${apiBaseUrl}/${id}`, updatedData);
        row.classList.remove('editing');
        const cells = row.querySelectorAll('td[contenteditable]');
        cells.forEach(cell => cell.setAttribute('contenteditable', 'false'));

        const editButton = row.querySelector('a[title="Save"]');
        editButton.innerHTML = '<i class="bi bi-pencil-fill"></i>'; // Иконка редактирования
        editButton.title = "Edit";

        // Удаляем кнопку отмены
        const cancelButton = row.querySelector('.cancel-button');
        if (cancelButton) cancelButton.remove();
    } catch (error) {
        console.error("Ошибка сохранения изменений:", error);
        alert("Не удалось сохранить изменения. Повторите попытку позже.");
    }
}

function cancelEditingComponenttype(row) {
    const cells = Array.from(row.querySelectorAll('td')).filter(cell => !cell.classList.contains('actions'));
    const originalData = JSON.parse(row.dataset.originalData);

    // Возвращаем исходные значения
    cells.forEach((cell, index) => {
        cell.innerText = originalData[index];
    });

    row.classList.remove('editing');

    const editButton = row.querySelector('a[title="Save"]');
    if (editButton) {
        editButton.innerHTML = '<i class="bi bi-pencil-fill"></i>';
        editButton.title = "Edit";
    }

    const cancelButton = row.querySelector('.cancel-button');
    if (cancelButton) cancelButton.remove();

    // Убираем обработчики событий, если редактирование отменено
    cells.forEach(cell => {
        cell.removeEventListener('click', openSelectModal);
    });
}

function openSelectModal(cell) {
    const type = 'componentType'; // У нас выбор только типов компонентов

    // Удаляем все открытые модальные окна, если они есть
    const existingModal = document.querySelector('.modal-list');
    if (existingModal) {
        existingModal.remove();
    }

    // Создаем новое модальное окно
    const modal = document.createElement('div');
    modal.classList.add('modal-list');
    modal.innerHTML = `
        <div class="modal-list-content">
            <div class="modal-list-header">
                <span class="close">&times;</span>
                <h2>Select ${type}</h2>
            </div>
            <div class="modal-list-body">
                <table id="select-table">
                    <!-- Данные для выбора загружаются динамически -->
                </table>
            </div>
        </div>
    `;
    document.body.appendChild(modal);

    modal.querySelector('.close').onclick = () => modal.remove();

    // Позиционируем модальное окно под ячейкой
    const cellRect = cell.getBoundingClientRect();
    modal.style.left = `${cellRect.left}px`;
    modal.style.top = `${cellRect.bottom + window.scrollY}px`; // Учитываем прокрутку страницы

    loadSelectData(type, cell);
}

async function loadSelectData(type, cell) {
    try {
        const response = await axios.get(`${apiBaseUrl}/${type}s`, {
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`,
            },
        });

        const table = document.getElementById('select-table');
        table.innerHTML = '';

        response.data.forEach(item => {
            const row = document.createElement('tr');
            row.dataset.id = item.id;
            row.innerHTML = `<td>${item.name}</td>`;
            row.onclick = () => selectItem(item, cell);
            table.appendChild(row);
        });

    } catch (error) {
        console.error("Ошибка загрузки данных:", error);
        alert("Не удалось загрузить данные. Повторите попытку позже.");
    }
}

function selectItem(item, cell) {
    cell.dataset.componentTypeId = item.id;
    cell.innerText = item.name;

    const modal = document.querySelector('.modal-list');
    if (modal) modal.remove();
}
