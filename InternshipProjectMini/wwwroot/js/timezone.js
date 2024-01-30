
fetch('/api/timezones') 
    .then(response => response.json())
    .then(data => {
        const timezoneSelect = document.getElementById('timezoneSelect');
        const timezoneInput = document.getElementById('timezoneInput');

       
        timezoneSelect.innerHTML = "";

        data.forEach(timezone => {
            const option = document.createElement('option');
            option.value = timezone;
            option.text = timezone;
            timezoneSelect.add(option);
        });
    })
    .catch(error => console.error('Error fetching timezones:', error));

function setSelectedTimezone() {
    
    timezoneInput.value = timezoneSelect.options[timezoneSelect.selectedIndex].text;
    $("#timezoneModal").modal("hide");
}
