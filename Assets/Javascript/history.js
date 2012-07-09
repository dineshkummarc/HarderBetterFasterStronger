/* Draws a line-plot using the data in "weights" using the specified activityName as the key.
* The second argument is the id of the canvas element to draw the graph in.
*/
function drawGraph(activityName, canvasId) {
    var canvas = document.getElementById(canvasId);

    if (canvas.getContext) {
        var ctx1 = canvas.getContext("2d");
        var ctx2 = canvas.getContext("2d");
        var pixelsPerDate = canvas.width / (weights[activityName].length - 1);
        var pixelsPerWeight = canvas.height / Math.max.apply(Math, weights[activityName]);

        ctx1.beginPath();

        for (i = 0; i < weights[activityName].length; i++) {
            var x = i * pixelsPerDate;
            var y = canvas.height - (weights[activityName][i] * pixelsPerWeight) / 2;

            if (i == 0) {
                ctx1.moveTo(x, y);
            } else {
                ctx1.lineTo(x, y);
            }

            if (i > 0 && i < weights[activityName].length - 1) {
                ctx2.moveTo(x, y);
                ctx2.arc(x, y, 2, 0, Math.PI * 2, true);
            }
        }

        ctx1.lineJoin = "round";
        ctx1.lineWidth = 2;
        ctx1.strokeStyle = "#FF0000";
        ctx1.stroke();
    }
};