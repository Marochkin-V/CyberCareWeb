const apiBaseUrl = "/api/Components"; // Базовый URL для Component
let currentPage = 1; // Текущая страница
const itemsPerPage = 10; // Количество записей на странице

async function loadData(page = 1) {
    try {
        // Запрос данных с сервера
        const response = await axios.get(`${apiBaseUrl}`, {
            params: {
                page,
                pageSize: itemsPerPage,
            }
        });

        // Создание переменных для таблицы
        const itemsLength = response.data.items.length;
        const totalCount = response.data.totalCount;
        const tableTitle = "Компоненты";
        const tableHead = `
            <tr>
                <th>Тип</th>
                <th>Бренд</th>
                <th>Производитель</th>
                <th>Характеристики</th>
                <th>Гарантийный срок (мес.)</th>
                <th>Цена</th>
                <th>Действия</th>
            </tr>
        `;
        const tableBody = response.data.items.map(item => `
            <tr data-id="${item.id}">
                <td data-field="componentType" data-component-type-id="${item.componentTypeId}">${item.componentType.name}</td>
                <td contenteditable="false">${item.brand}</td>
                <td contenteditable="false">${item.manufactorer}</td>
                <td contenteditable="false">${item.specifications}</td>
                <td contenteditable="false">${item.warrantyPeriod}</td>
                <td contenteditable="false">${item.price.toFixed(2)} ₽</td>
                <td class="actions">
                    <a class="edit-buttons" href="javascript:void(0);" onclick="editRow(this)" title="Edit">
                        <i class="bi bi-pencil-fill"></i>
                    </a>
                    <a href="javascript:void(0);" onclick="info(this)" title="View Details">
                        <i class="bi bi-eye-fill"></i>
                    </a>
                </td>
            </tr>
        `).join('');

        // Создание таблицы
        createTable(itemsLength, totalCount, page, tableTitle, tableHead, tableBody);
    } catch (error) {
        console.error("Ошибка загрузки данных:", error);
    }
}

// Инициализация
loadData();
