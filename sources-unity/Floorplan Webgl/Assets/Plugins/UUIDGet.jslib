mergeInto(LibraryManager.library, {
  GetUUID: function() {
    window.alert("Grabbing UUID");
    let Company = prompt("Please input your company name");
    let url = "https://floorspaces.azurewebsites.net/dev/GetUUID/" + Company;
    
    fetch(url).then(function(response) {
      return response.text(); // Use response.text() instead of response.body.toString()
    }).then(function(data) {
      console.log(data);
      data = data.replace('[', '');
      data = data.replace(']', '');
      let UUID = parseInt(data);
      console.log(UUID);
      return UUID;
    });
  }
});
