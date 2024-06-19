mergeInto(LibraryManager.library, {
  AskShapeScripts: function() {
    let url = "http://localhost:5269/dev/GrabAssetBundle/";
    
    fetch(url).then(function(response) {
      return response.text(); // Use response.text() instead of response.body.toString()
    })
    .then(function(data) 
    {
      let scriptArray = data.split("END")
      return scriptArray;      
    });
  }
});