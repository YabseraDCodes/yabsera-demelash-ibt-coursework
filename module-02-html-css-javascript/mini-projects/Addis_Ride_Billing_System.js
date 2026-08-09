'use strict';

const sumDistance = (...distances) => {
    let totalDistance = 0;
    for(let i=0; i<distances.length; i++){
        totalDistance += distances[i];
    }
    return totalDistance;
};



const calculateBaseFare = (totalDistance, ratePerKm = 15) => totalDistance * ratePerKm
const formatCurrency = (amount) => `${Number(amount).toFixed(2)} ETB`


function makeSurgeMultiplier(surgeRate) {
    return function (basefare) {
        return surgeRate * basefare
    }
}


function makeDriverTracker() {
    let tripsCompleted = 0

    return {
        recordTrip: function () {
            tripsCompleted++
        },
        getTrips: function () {
            return tripsCompleted;
        }
    }
}


function generateReceipt(distancesArray, surgeFunction,
    driverTracker, onReceiptReady) {
    driverTracker.recordTrip();
    let totalDistance = sumDistance(...distancesArray);
    let basefare = calculateBaseFare(totalDistance);
    let finalFare = surgeFunction(basefare)
    onReceiptReady( `Trip #${driverTracker.getTrips()}
        Total Fare is ${formatCurrency(finalFare)}.
    `)
}

const tayesTracker = makeDriverTracker();
const standardPricing = makeSurgeMultiplier(1.0);
const rushHourPricing = makeSurgeMultiplier(1.5)

const printToConsole = (message) => console.log(message);

generateReceipt([2, 3], standardPricing, tayesTracker, printToConsole);

generateReceipt([10], rushHourPricing, tayesTracker, printToConsole);
