import format, { withVat } from "./pricing.js";

const orders = [
    {
        id: 101,
        items: [
            { price: 250, qty: 2 },
            { price: 100, qty: 1 }
        ]
    },
    {
        id: 102,
        items: [
            { price: 300, qty: 2 },
            { price: 150, qty: 1 }
        ]
    },
    {
        id: 103,
        items: [
            { price: 120, qty: 2 },
            { price: 80, qty: 1 }
        ]
    }
];

const total = ({ items }) =>
    items.reduce(
        (sum, { price, qty }) => sum + price * qty,
        0
    );

const ordersWithTotal = orders.map(order => ({
    ...order,
    total: total(order)
}));

const expensiveOrders = ordersWithTotal.filter(
    order => order.total > 500
);

const grandTotal = ordersWithTotal.reduce(
    (sum, order) => sum + order.total,
    0
);

console.log("Orders over 500 ETB:");

expensiveOrders.forEach(order => {
    console.log(`#${order.id}: ${format(order.total)}`);
});

console.log(`Grand Total: ${format(grandTotal)}`);
console.log(`Grand Total with VAT: ${format(withVat(grandTotal))}`);
