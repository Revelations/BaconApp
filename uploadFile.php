<?php
//assuming upload.php and upload folder are in the same dir
$uploaddir = 'uploads/';

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

//if ($_FILES['userfile']['size']> 300000)     //Limiting image at 300K
//{
 //   exit("Your file is too large."); 
//}

// Add support here for PNG files:
//if ((!($_FILES['userfile']['type'] == "text/plain")) &&  //Also allowing 
//    (!($_FILES['userfile']['type'] == "text/plist")) &&  //plist files
//    (!($_FILES['userfile']['type'] == "text/html")))     //HTML files
//{
//    exit("Incorrect file type.  " . $_FILES['userfile']['type'] . " is the file type you uploaded."); 
//}

if (move_uploaded_file($_FILES['userfile']['tmp_name'], $uploadfile)) {
    $postsize = ini_get('post_max_size');    //Not necessary, I was using these
    $canupload = ini_get('file_uploads');    //server variables to see what was 
    $tempdir = ini_get('upload_tmp_dir');    //going wrong.
    $maxsize = ini_get('upload_max_filesize');
    echo "http://www.yourwebsite.com/uploads/{$file}" . "\r\n file size" . $_FILES['userfile']['size'] . "\r\n" . $_FILES['userfile']['type'] ;
}
?>

//<?php

//  if ($_FILES["file"]["error"] > 0)
//    {
//    echo "Return Code: " . $_FILES["file"]["error"] . "<br />";
//    }
 // else
//    {
//    echo "Upload: " . $_FILES["file"]["name"] . "<br />";
//    echo "Type: " . $_FILES["file"]["type"] . "<br />";
//    echo "Size: " . ($_FILES["file"]["size"] / 1024) . " Kb<br />";
//    echo "Temp file: " . $_FILES["file"]["tmp_name"] . "<br />";

//    if (file_exists("upload/" . $_FILES["file"]["name"]))
//      {
//      echo $_FILES["file"]["name"] . " already exists. ";
//      }
//    else
//      {
//      move_uploaded_file($_FILES["file"]["tmp_name"],
 //     "upload/" . $_FILES["file"]["name"]);
  //    echo "Stored in: " . "upload/" . $_FILES["file"]["name"];
  //    }
   // }//
?>