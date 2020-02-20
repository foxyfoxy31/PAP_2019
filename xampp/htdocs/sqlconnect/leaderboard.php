<?php
    $con = new mysqli('localhost', 'root', '', 'unitydatabase');
    if ($con -> connect_errno) {
        echo "Failed to connect to MySQL: " . $con -> connect_error;
        exit();
    }
    $sql = "SELECT playername, leveltime FROM `leaderboard` ORDER BY leveltime ASC";
    $result = $con->query($sql);
    if ($result->num_rows > 0) {
        while($row = $result->fetch_assoc()) {
            echo "Name:" . $row["playername"] . "|Time:" . $row["leveltime"] . ";";
        }
    } else {
        echo "0 results";
    }
?>