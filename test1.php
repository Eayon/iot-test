<html>
	<head>
	<script type="text/javascript" src="jquery-1.8.3.min.js"></script>
		<script>
 $(function(){
     var my_data="前台变量";
    // my_data=escape(my_data)+"";//编码，防止汉字乱码
     $.ajax({
         url: "ajax_php.php",  
         type: "POST",
         data:{trans_data:my_data},
         //dataType: "json",
         error: function(){  
             alert('Error loading XML document');  
         },  
         success: function(data,status){//如果调用php成功    
             //alert(unescape(data));//解码，显示汉字
             alert(data);
         }
     });
     
 });

 </script>
	</head>
	<body>
		
	</body>
</html>
