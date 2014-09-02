<?php
    // header('Content-Type:text/html; charset=gb2312');//使用gb2312编码，使中文不会变成乱码
     //$backValue=$_POST['trans_data'];
     //echo $backValue."+后台返回";

     $r = new COM('KLCOM_N1000.DataTrans');
     $data = $r->GetData('zd');
     echo $data;
     //echo (rand(0,10));
 ?>