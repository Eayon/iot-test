<?php
    // header('Content-Type:text/html; charset=gb2312');//使用gb2312编码，使中文不会变成乱码
     $backValue=$_POST['trans_data'];
     //echo $backValue."+后台返回";

     $r = new COM('KLCOM_N1000.DataTrans');

     $zd = $r->GetData('zd');
     $trsd = $r->GetData('trsd');
     $trwd = $r->GetData('trwd');
     $eyht = $r->GetData('eyht');
     $kqsd = $r->GetData('kqsd');
     $kqwd = $r->GetData('kqwd');
     
     $arr = array('zd' =>$zd, 'trsd'=>$trsd,'trwd'=>$trwd,'eyht'=>$eyht,'kqsd'=>$kqsd,'kqwd'=>$kqwd);
     
     if (empty($arr)) {
          echo "";
     }
     else{
     $json_arr = json_encode($arr);
     echo $json_arr;
     }
     
     //echo $data;
     //echo (rand(0,10));


 ?>