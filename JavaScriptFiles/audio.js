function displayDate()
{
document.write(Date());
}

function playSound(path){
  document.write("<audio id=audioPlayer controls=controls> Your browser does not support the audio element.</audio>");
  document.getElementById('audioPlayer').src = path;
  document.getElementById('audioPlayer').play();
}