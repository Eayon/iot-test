<html>
<head>
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<script type="text/javascript" src="jquery-1.8.3.min.js"></script>
<script type="text/javascript" src="highcharts.js"></script>
 <script type="text/javascript" src="exporting.js"></script>
 <script type="text/javascript" src="demo.js"></script>
  <link rel="stylesheet" type="text/css"  href="reset.css" />
 <link rel="stylesheet" type="text/css" href="demo.css"/>
<script>
function ShowOrHideDiv (id) {
    // body...
  var cc=document.getElementById(id);
  var alldiv = document.getElementsByName("container");
  var str = cc.id;
  var num = str.substr(str.length-1,1);
  var hea =str.substr(0,9);
  var name = hea +num;
  for (var i = 0; i < alldiv.length; i++) {
        if (alldiv[i].id == name) {
            alldiv[i].style.display="block";
        } else{
            alldiv[i].style.display="none";
        };
  };


/*  if (cc) {
    if (cc.style.display=="none") {
        cc.style.display="block";  

       // var a = cc.id.charAt(cc.id.length-1);
        var str = cc.id;
        var num = str.substr(str.length-1,1);
        var name =str.substr(0,9);
        //alert(name);
      
    }else{
        cc.style.display="none";
   
    }*/
/*str.subStr(str.length-1,1)*/



 // }
}
                                                                                            

   </script>
</head>
<body>
    <div class="header" id="header">
        <input type="button" id="btngz"   class="btn" value="照  度" OnClick='ShowOrHideDiv("container0")';></input>
        <input type="button" id="btntrsd" class="btn" value="土壤湿度" OnClick='ShowOrHideDiv("container1")';></input>
        <input type="button" id="btntrwd" class="btn" value="土壤温度" OnClick='ShowOrHideDiv("container2")';></input>
        <input type="button" id="btneyht" class="btn" value="二氧化碳" OnClick='ShowOrHideDiv("container3")';></input>
        <input type="button" id="btnkqsd" class="btn" value="空气湿度" OnClick='ShowOrHideDiv("container4")';></input>
        <input type="button" id="btnkqwd" class="btn" value="空气温度" OnClick='ShowOrHideDiv("container5")';></input>
    </div>
    <div class="col" id="col">
        <div id="container0" name="container" style="min-width: 800px; height: 400px; float: left;display:none"></div>
        <div id="container1" name="container"  style="min-width: 800px; height: 400px; float: left;display:none"></div>
        <div id="container2" name="container"  style="min-width: 800px; height: 400px; float: left;display:none"></div>
        <div id="container3" name="container"  style="min-width: 800px; height: 400px; float: left;display:none"></div>
        <div id="container4" name="container"  style="min-width: 800px; height: 400px; float: left;display:none"></div>
        <div id="container5" name="container"  style="min-width: 800px; height: 400px; float: left;display:none"></div>
    </div>
</body>
</html>
