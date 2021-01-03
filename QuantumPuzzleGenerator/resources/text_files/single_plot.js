function drawVisualization() {

    var options = {
        width: "100%",
        height: "100%",
        style: "dot-color",
        showLegend: false,
        dotSizeRatio: 0.035,
        dataColor: {
            strokeWidth: 10
        },
        xValueLabel: function (_) {
            return '';
        },
        yValueLabel: function (_) {
            return '';
        },
        zValueLabel: function (_) {
            return '';
        },
        xLabel: 'left/right',
        yLabel: 'front/back',
        zLabel: 'up/down',
        xMin: 0.0,
        xMax: 1.0,
        yMin: 0.0,
        yMax: 1.0,
        zMin: 0.0,
        zMax: 1.0,
        cameraPosition: {
            horizontal: 0.3,
            vertical: 0.7,
            distance: 1.65
        }
    };

    var container = document.getElementById('mygraph');
    graph3d = new vis.Graph3d(container, generatedByQuantumPuzzles, options);
}
