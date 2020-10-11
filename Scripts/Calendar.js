function generateCalendar(events) {
    $("#calendar").fullCalendar({
        defaultView: 'month',
        contentHeight: 600,
        businessHours: true,
        header: {
            left: 'month,agendaWeek, today',
            center: 'title',
            right: 'prevYear, prev,next,nextYear'
        },
        timeFormat: "h(:mm)a",
        events: events,
        eventClick: function (event) {
            console.log(event);
            $("#eventName").text(event.title);
            $("#eventDesc").text(event.description);
            $("#startTime").text("Start time: " + moment(event.start).format("DD-MMM-YYYY HH:mm"));
            if (event.start < $.now()) {
                $('#bookLink').text("Event finished");
                $('#bookLink').attr("onclick", "return false");
            } else {
                $('#bookLink').text("Book Now");
                var newUrl = "/BookEvents/Book/" + event.eventId;
                $('#bookLink').attr("href", newUrl);
            }

        }
    });
}