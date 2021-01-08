var canvas = document.getElementById("myCanvas");
var graphics = canvas.getContext("2d");

var CX = canvas.width / 2
var CY = canvas.height / 2

for (var i = 0; i < 360; i += 0.1) {
    var initNominator = i * (2*Math.PI)
    var modifiedNominator = initNominator + 1115;
    var rad = modifiedNominator / 360;
    graphics.strokeStyle = "hsla(" + i + ", 100%, 50%, 1.0)";
    graphics.beginPath();
    graphics.moveTo(CX, CY);
    graphics.lineTo(CX + CX * Math.cos(rad), CY + CY * Math.sin(rad));
    graphics.stroke();
}

graphics.strokeStyle = "white";
graphics.fillStyle = 'white';
graphics.beginPath();
graphics.arc(CX, CY, CX * 0.8, 0, 2 * Math.PI);
graphics.fill();
