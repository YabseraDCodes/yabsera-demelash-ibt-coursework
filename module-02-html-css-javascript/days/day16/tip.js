//Run in command like: node tip.js <bill> <partySize> <paymentMethod>

let bill = Number(process.argv[2]);
let partySize = Number(process.argv[3]);
let paymentMethod = process.argv[4];

let tip;

if (bill > 300) {
    tip = bill * 0.10;
} else {
    tip = bill * 0.05;
}

let total = bill + tip;
let serviceFee = 0;

switch (paymentMethod) {
    case "TeleBirr":
        serviceFee = 5;
        break;

    case "CBE":
        serviceFee = 3;
        break;

    default:
        serviceFee = 0;
}

total = total + serviceFee;
let perPerson = total / partySize;

console.log(`Bill: ${bill} ETB
Tip: ${tip.toFixed(2)} ETB
Service Fee: ${serviceFee} ETB
Total: ${total.toFixed(2)} ETB
Each Person Pays: ${perPerson.toFixed(2)} ETB`);