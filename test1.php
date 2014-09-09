<html>
	<head>
	<script type="text/javascript" src="jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="highcharts.js"></script>
 <script type="text/javascript" src="exporting.js"></script>
		<script>
$(function () {                                                                     
    $(document).ready(function() {                                                  
        Highcharts.setOptions({                                                     
            global: {                                                               
                useUTC: false                                                       
            }                                                                       
        });                                                                         
                                                                                    
        var chart;                                                                  
        $('#container').highcharts({                                                
            chart: {                                                                
                type: 'spline',                                                     
                animation: Highcharts.svg, // don't animate in old IE               
                marginRight: 10,                                                    
                events: {                                                           
                    load: function() {    
                        // set up the updating of the chart each second             
                        var series = this.series[0];                                
                        setInterval(function() { 
                            var my_data="前台变量";
                            $.ajax({
                              url: "ajax_php.php",  
                             type: "POST",
                             data:{trans_data:my_data},
                             dataType: "json",
                             error: function(){  
                                 //alert('error~');  
                             },  
                             success: function(data,status){//如果调用php成功    
                                 //alert(unescape(data));//解码，显示汉字
                                 //var y = unescape(data);
                                 var x = (new Date()).getTime(); //, y =Math.random()*10;
                                 if(data)
                                 {
                                 var y =data.zd;  

                                 }
                                 else
                                 {
                                    alert("a");
                                 }        
                                 series.addPoint([x,Number(y)], true, true); 
                             }
                         });        
                          //  var x = (new Date()).getTime(), // current time         
                            //    y = Math.random();   
                          // series.addPoint([x,y], true, true);                    
                        }, 2000);                                                   
                    }                                                               
                }                                                                   
            },                                                                      
            title: {                                                                
                text: '照度'                                            
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
                formatter: function() {                                             
                        return '<b>'+ this.series.name +'</b><br/>'+                
                        Highcharts.dateFormat('%Y-%m-%d %H:%M:%S', this.x) +'<br/>'+
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
                data: (function() {                                                 
                    // generate an array of random data                             
                    var data = [],                                                  
                        time = (new Date()).getTime(),                              
                        i;                                                          
                                                                                    
                    for (i = -19; i <= 0; i++) {                                    
                        data.push({                                                 
                            x: time + i * 1000,                                     
                            y: Math.random()                                        
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
        <div id="container" style="min-width: 800px; height: 400px; float: left;display:block"></div>
	</body>
</html>
