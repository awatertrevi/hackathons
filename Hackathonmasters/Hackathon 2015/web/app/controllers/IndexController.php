<?php

/**
 * Namespace
 * @copyright (c) 2014 - 2015 | Paradoxis
 */
namespace Paradoxis\Controllers;

/**
 * Use classes
 */
// None

/**
 * Class IndexController
 * @version 0.1.0
 * @package Paradoxis\Controllers
 * @author  Paradoxis <luke@paradoxis.nl>
 */
class IndexController extends ControllerBase {


	/**
	 * Initialize component
	 * @return void
	 */
	public function initialize() {
		parent::initialize();
	}

	/**
	 * Index route "/"
	 * @return void
	 */
	public function indexAction() {
		if ($this->session->get('logged-in') == true) {
			$this->response->redirect('dashboard/home');
		} else {
			$this->response->redirect('dashboard/login');
		}
	}

	/**
	 * 404 Not found route
	 * @return void
	 */
	public function notFoundAction() {
		$uri = $this->request->getURI();
		if (strlen($uri) > 4 && substr($uri, 0, 4) == "/api") {
			$this->fail(ApiController::ERR_INVALID_CALL);
		} else {
			$this->response->setStatusCode(404, "Not Found");
			exit("<h1>Not Found (404)</h1><p>The page you requested does not exist.</p>");
		}
	}
}

