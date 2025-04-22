$(document).ready(function () {
    // Add new task
    $("#add-task-btn").click(function () {
        var taskName = $("#task-name").val();
        var selectedTag = $("input[name='selectedTag']:checked").val();
        var selectedTagName = $("input[name='selectedTag']:checked").next("label").text(); // Get the tag name
        var taskDeadline = $("#task-deadline").val();  // Get the selected date

        if (!taskName) {
            alert("Please enter a task name.");
            return;
        }
        if (!selectedTag) {
            alert("Please select a tag.");
            return;
        }

        $.ajax({
            url: "/Home/Create",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({
                Title: taskName,
                TagId: parseInt(selectedTag),
                Deadline: taskDeadline 
            }),
            success: function (response) {
                if (response.success) {
                    // Clear inputs
                    $("#task-name").val("");
                    $("#task-deadline").val("");  // Clear the deadline input
                    $("input[name='selectedTag']").prop("checked", false);

                    // Create new task card with deadline
                    var trashIconUrl = '@Url.Content("~/images/trash.png")';  // Generate the correct path for trash icon
                    var newTask = `
                        <div class="task-card" id="task-${response.taskId}" data-task-id="${response.taskId}" draggable="true">
                            <div>
                                <h5>${taskName}</h5>
                                <div class="task-card-tag">${selectedTagName}</div>
                                ${response.deadline ? `<div class="task-card-deadline">Due to: ${response.deadline}</div>` : ""}
                            </div>
                            <img src="${trashIconUrl}" />
                        </div>`;

                    // Append new task to "To Do" column
                    $(".column[data-column-id='1'] .task-list").append(newTask);
                }
            },
            error: function () {
                alert("Error creating task.");
            }
        });
    });

    // Handle click on trash icon to delete task
    $(".task-list").on("click", ".task-card img", function () {
        var taskCard = $(this).closest(".task-card"); // Find the task card element
        var taskId = taskCard.data("task-id"); // Get the task ID from the data attribute

        if (confirm("Are you sure you want to delete this task?")) {
            $.ajax({
                url: "/Home/DeleteTask",  // URL of the delete action
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify({ taskId: taskId }),  // Send the task ID to the server
                success: function (response) {
                    if (response.success) {
                        // Remove the task card from the UI
                        taskCard.remove();
                    } else {
                        alert("Error: " + response.message);
                    }
                },
                error: function () {
                    alert("Error deleting task.");
                }
            });
        }
    });

    // Allow dropping tasks into columns
    $(".column").on("dragover", function (event) {
        event.preventDefault(); // Allow items to be dropped
    });

    // Start dragging a task card
    $(".task-list").on("dragstart", ".task-card", function (event) {
        event.originalEvent.dataTransfer.setData("taskId", event.target.id); // Store dragged task ID
    });

    // Drop a task into a new column
    $(".column").on("drop", function (event) {
        event.preventDefault();
        var taskId = event.originalEvent.dataTransfer.getData("taskId"); // Get dragged task ID
        var column = event.target.closest(".column"); // Get the column where dropped
        var columnId = column.getAttribute("data-column-id");

        if (taskId && columnId) {
            // Move task visually
            var draggedTask = document.getElementById(taskId);
            column.querySelector(".task-list").appendChild(draggedTask);

            // Send AJAX request to update the task's ColumnId in the database
            $.ajax({
                url: "/Home/UpdateTaskColumn",  // Endpoint to update ColumnId
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify({
                    taskId: parseInt(taskId.replace("task-", "")), // Extract task ID
                    columnId: parseInt(columnId)  // New ColumnId
                }),
                success: function (response) {
                    if (response.success) {
                        console.log("Task moved successfully!");
                    } else {
                        alert("Error: " + response.message);
                    }
                },
                error: function () {
                    alert("Error updating task column.");
                }
            });
        }
    });
});
