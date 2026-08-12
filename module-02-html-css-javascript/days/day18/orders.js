const order = {
    id: 1042,
    customer: "Tigist Mengistu",
    items: [
        { name: "Tibs", qty: 2, price: 200 },
        { name: "Shiro", qty: 1, price: 120 },
    ],
};
order.items[0].name;
order.items.length;
// "Tibs"
// 2
order.items
    .reduce((s, i) => s + i.qty * i.price, 0);
// 520 ETB
console.log(order.items[1].name)