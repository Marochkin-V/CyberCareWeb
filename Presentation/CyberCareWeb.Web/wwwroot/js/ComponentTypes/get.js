﻿const apiBaseUrl = "/api/ComponentTypes"; // Базовый URL для ComponentType
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
        const tableTitle = "Типы комплектующих";
        const tableHead = `
            <tr>
                <th>Название</th>
                <th>Описание</th>
                <th>Действия</th>
            </tr>
        `;
        const tableBody = response.data.items.map(item => `
            <tr data-id="${item.id}">
                <td contenteditable="false">${item.name}</td>
                <td contenteditable="false">${item.description}</td>
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
