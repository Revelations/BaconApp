<?php
//assuming upload.php and upload folder are in the same dir
$uploaddir = 'uploads/';

if(!is_dir($uploaddir)){
mkdir('uploads/');
}

$file = basename($_FILES['userfile']['name']);
$uploadfile = $uploaddir . $file;
$product = $_GET[product];
echo "hello";

if (is_uploaded_file($_FILES['userfile']['tmp_name']))
{
    echo "File uploaded. \r\n";
} else {
    echo "File not uploaded. \r\n";
}

if (move_uploaded_file($_FILES['userfile']['tmp_name'], $uploadfile)) {
    $postsize = ini_get('post_max_size');    //Not necessary, I was using these
    $canupload = ini_get('file_uploads');    //server variables to see what was 
    $tempdir = ini_get('upload_tmp_dir');    //going wrong.
    $maxsize = ini_get('upload_max_filesize');
    echo "http://www.yourwebsite.com/uploads/{$file}" . "\r\n file size" . $_FILES['userfile']['size'] . "\r\n" . $_FILES['userfile']['type'] ;
}
?>
