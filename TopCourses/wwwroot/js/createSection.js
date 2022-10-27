$("#addSection").click(function () {
    let courseTitle = document.getElementById("courseTitle").value;
    let courseSubTitle = document.getElementById("courseSubTitle").value;
    let courseImageUrl = document.getElementById("courseImageUrl").value;
    let courseLevel = document.getElementById("courseLevel").value;
    let courseDescription = document.getElementById("courseDescription").value;
    let price = document.getElementById("price").value;

    let sectionTitle = document.getElementById("sectionTitle").value;
    let videoUrl = document.getElementById("sectionVideoUrl").value;
    let sectionDescription = document.getElementById("sectionDescription").value;
    debugger
    let course = {
        title: courseTitle,
        subtitle: courseSubTitle,
        imageUrl: courseImageUrl,
        level: courseLevel,
        description: courseDescription,
        price: price,
        section: {
            sectionTitle: sectionTitle,
            videoUrl: videoUrl,
            sectionDescription: sectionDescription
        }
    }

    debugger
        $.ajax({
            type: "POST",
            url: 'https://localhost:7062/Course/CreateSection', // get the link to the controller's action method
            data: JSON.stringify(course),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            //success: function (result) {
            //    alert('Successfully received Data ');
            //    console.log(result);
            //},
            //error: function () {
            //    alert('Failed to receive the Data');
            //    console.log('Failed ');
        });
    alert(this.id);
});