const apiBaseUrl = "/api/Orders"; // Базовый URL для Order
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
        const tableTitle = "Заказы";
        const tableHead = `
            <tr>
                <th>Дата заказа</th>
                <th>Статус оплаты</th>
                <th>Статус выполнения</th>
                <th>Общая стоимость</th>
                <th>Гарантийный срок (мес.)</th>
                <th>Действия</th>
            </tr>
        `;
        const tableBody = response.data.items.map(item => `
            <tr data-id="${item.id}">
                <td contenteditable="false">${new Date(item.orderDate).toLocaleDateString()}</td>
                <td contenteditable="false">${item.paymentStatus ? "Оплачено" : "Не оплачено"}</td>
                <td contenteditable="false">${item.completionStatus ? "Завершён" : "В процессе"}</td>
                <td contenteditable="false">${item.totalCost.toFixed(2)} ₽</td>
                <td contenteditable="false">${item.warrantyPeriod}</td>
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
