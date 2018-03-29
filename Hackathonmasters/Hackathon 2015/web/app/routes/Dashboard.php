<?php

/**
 * Namespace
 * @copyright (c) 2014 - 2015 | Paradoxis
 */
namespace Paradoxis\Routes;

/**
 * Use classes
 * @package Phalcon
 */
use \Phalcon\Mvc\Router\Group;

/**
 * Class Dashboard
 * @version 0.1.0
 * @package Paradoxis\Routes
 * @author  Paradoxis <luke@paradoxis.nl>
 */
class Dashboard extends Group {

	/**
	 * Initialize api auth routes
	 * @return void
	 */
	public function initialize() {
		$this->setPrefix('/dashboard');
		$this->add('/home', 'Dashboard::home');
		$this->add('/login', 'Dashboard::login');
		$this->add('/patients', 'Dashboard::patients');
		$this->add('/appointments', 'Dashboard::appointments');
	}
}