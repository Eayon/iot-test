<html>
<head>
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<script type="text/javascript" src="jquery-1.8.3.min.js"></script>
<script type="text/javascript" src="highcharts.js"></script>
 <script type="text/javascript" src="exporting.js"></script>
<script>
    $(function () {
        $(document).ready(function () {
            Highcharts.setOptions({
                global: {
                    useUTC: false
                }
            });

            var chart;
            $('#container').highcharts({
                chart: {
                    width: 700,
                    backgroundColor: '#FCFFC5',
                    type: 'spline',
                    animation: Highcharts.svg, // don't animate in old IE               
                    marginRight: 10,
                    events: {
                        load: function () {
                            // set up the updating of the chart each second             
                            var series = this.series[0];
                            setInterval(function () {
                                $.ajax({
                                    //     var x = (new Date()).getTime(), // current time         
                                    //      y = Math.random();
                                    // y = rid;
                                    series.addPoint([x, y], true, true);
                                    url: "Handler1.ashx?flag=1",
                                    type: "get",
                                    dataType: "json",
                                    success: function (data, textStatus, XMLHttpRequest) {
                                        var x = (new Date()).getTime(),
                                            y = data;
                                        ;
                                      //  series.addPoint([x, y], true, true);
                                        //alert(data);

                                    },
                                    error: function () { }

                                });

                            }, 1000);
                        }
                    }
                },
                title: {
                    text: '日照温度图表数据'
                },
                xAxis: {
                    type: 'datetime',
                    tickPixelInterval: 150
                },
                yAxis: {
                    title: {
                        text: 'Value'
                    },
                    plotLines: [{
                        value: 0,
                        width: 1,
                        color: '#808080'
                    }]
                },
                tooltip: {
                    formatter: function () {
                        return '<b>' + this.series.name + '</b><br>' +
                        Highcharts.dateFormat('%Y-%m-%d %H:%M:%S', this.x) + '<br>' +
                        Highcharts.numberFormat(this.y, 2);
                    }
                },
                legend: {
                    enabled: false
                },
                exporting: {
                    enabled: false
                },
                series: [{
                    name: 'Random data',
                    data: (function () {
                        // generate an array of random data                             
                        var data = [],
                            time = (new Date()).getTime(),
                            i,
                        jsonData = 10; //<%= jsondata %>;

                        for (i = -19; i <= 0; i++) {

                            data.push({
                                x: time + i * 1000,
                                //y: Math.random()
                                y: jsonData
                            });

                        }
                        return data;
                    })()
                }]
            });
        });

   });

   </script>
</head>
<body>
<div id="container" style="min-width: 800px; height: 400px; float: left"></div>
</body>
</html>
