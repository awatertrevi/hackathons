<?php

/**
 * Namespace
 * @copyright (c) 2014 - 2015 | Paradoxis
 */
namespace Paradoxis\Controllers;

/**
 * Use classes
 */
use \Phalcon\Mvc\Controller;
use \Phalcon\Http\Response;


/**
 * Class ControllerBase
 * @version 0.2.0
 * @package Paradoxis\Controllers
 * @author  Paradoxis <luke@paradoxis.nl>
 */
class ControllerBase extends Controller {

	/**
	 * Constructor
	 * @return void
	 */
	public function initialize() {

		/**
		 * Check if the request URI is the domains IP address
		 * @return void
         */
		if ($this->request->getServerName() === "92.222.19.24") {
			$this->response->redirect("https://www.paradoxis.nl/", true, 301);
			$this->response->send();
		}

		/**
		 * Meta information
		 * @var string
		 */
		$this->view->setVar('title', '');
		$this->view->setVar('robots', 'index, follow');

		/**
		 * Change the default views directory to /app/views/pages/
		 * @var string
		 */
		$moduleName = $this->dispatcher->getControllerName();
		$actionName = $this->dispatcher->getActionName();
		$this->view->pick("pages/$moduleName/$actionName");
	}

	/**
	 * Return a json success message with body
	 * @param mixed $body
	 * @param string $message
	 * @return \Phalcon\Http\Response
	 */
	public function success($body = null, $message = "API call successful") {
		$this->view->disable();

		$response = new Response();
		$response->setContentType('application/json');
		$response->setContent(json_encode([
			"success" => true,
			"message" => $message,
			"body" => $body
		]));

		return $response->send();
	}

	/**
	 * Return a json fail message without body
	 * @param string $message
	 * @return \Phalcon\Http\Response
	 */
	public function fail($message = "API call failed") {
		$this->view->disable();

		$response = new Response();
		$response->setContentType('application/json');
		$response->setContent(json_encode([
			"success" => false,
			"message" => $message
		]));

		return $response->send();
	}
}
