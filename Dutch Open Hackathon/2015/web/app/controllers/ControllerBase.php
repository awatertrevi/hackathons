<?php

namespace Hackathon\Controllers;

use \Phalcon\Mvc\Controller;
use \Phalcon\Http\Response;

/**
 * Class ControllerBase
 *
 * @package Hackathon\Controllers
 * @author  Trevi Awater <trevi@modision.com>
 */
class ControllerBase extends Controller
{
	/**
	 * Constructor
	 *
	 * @return void
	 */
	public function initialize()
	{
		
	}


   /**
	 * Return a json fail message without body.
	 *
	 * @author Luke Paris <luke@paradoxis.nl>
	 *
	 * @param string $message
	 * @return \Phalcon\Http\Response
	 */
	public function fail($message = "Call failed.")
	{
		$this->view->disable();

		$response = new Response();
		$response->setContentType('application/json');
		$response->setContent(json_encode(
		[
			"success" => false,
			"message" => $message
		]));

		return $response->send();
	}


	/**
	 * Return a json success message with body.
	 *
	 * @author Luke Paris <luke@paradoxis.nl>
	 *
	 * @param mixed $body
	 * @param string $message
	 * @return \Phalcon\Http\Response
	 */
	public function success($body = null, $message = "Call was successful.")
	{
		$this->view->disable();

		$response = new Response();
		$response->setContentType('application/json');
		$response->setContent(json_encode(
		[
			"success" => true,
			"message" => $message,
			"body" => $body
		]));

		return $response->send();
	}
}
