function drawVisualization() {

    var options = {
        width: "100%",
        height: "100%",
        style: "dot-color",
        showLegend: false,
        dotSizeRatio: 0.04 * generatedByQuantumPuzzles.dotSizeRatioScale,
        dataColor: {
            strokeWidth: 8
        },
        showXAxis: false,
        showYAxis: false,
        showZAxis: false,
        xMin: 0.0,
        xMax: 1.0,
        yMin: 0.0,
        yMax: 1.0,
        zMin: 0.0,
        zMax: 1.0,
        cameraPosition: {
            horizontal: -0.5,
            vertical: 0.175,
            distance: 1.5
        }
    };

    var container = document.getElementById('mygraph');
    graph3d = new vis.Graph3d(container, generatedByQuantumPuzzles.data, options);
}
