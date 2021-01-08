var x1 = generatedByQuantumPuzzles.x1;
var y1 = generatedByQuantumPuzzles.y1;
var x2 = generatedByQuantumPuzzles.x2;
var y2 = generatedByQuantumPuzzles.y2;

graphics.beginPath();
graphics.lineWidth = 8;
graphics.strokeStyle = "black";
graphics.lineCap = "round";
graphics.moveTo(CX, CY);
graphics.lineTo(CX + CX * x1, CY + CY * y1);
graphics.stroke();

graphics.beginPath();
graphics.lineWidth = 4;
graphics.strokeStyle = "gray";
graphics.moveTo(CX, CY);
graphics.lineTo(CX + CX * x2, CY + CY * y2);
graphics.stroke();
