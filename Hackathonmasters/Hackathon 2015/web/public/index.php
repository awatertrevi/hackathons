<?php

# Log everything
# Display nothing
error_reporting(E_ALL);

try {

    /**
     * Read the configuration
     */
    $config = include __DIR__ . "/../app/config/config.php";

    /**
     * Read auto-loader
     */
    include __DIR__ . "/../app/config/loader.php";

    /**
     * Read services
     */
    include __DIR__ . "/../app/config/services.php";

	/**
     * Handle the request
     */
    $application = new \Phalcon\Mvc\Application($di);
    echo $application->handle()->getContent();
} catch (\Exception $e) {
	header("HTTP/1.0 500 Internal Server Error");
	echo "<h1>Internal Server Error (500)</h1>";
	echo "<pre>";
	echo "Error in ".$e->getFile()." on line ".$e->getLine().".\n";
	echo "Error code ".$e->getCode()." with the message '<b>".$e->getMessage()."</b>'.\n\n";
	echo "Stack trace:\n";
	echo $e->getTraceAsString();
	echo "</pre>";
}
