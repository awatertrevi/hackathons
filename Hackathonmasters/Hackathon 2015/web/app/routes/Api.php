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
 * Class API
 * @version 0.1.0
 * @package Paradoxis\Routes
 * @author  Paradoxis <luke@paradoxis.nl>
 */
class Api extends Group {

	/**
	 * Initialize api auth routes
	 * @return void
	 */
	public function initialize() {
		$this->setPrefix('/api');
		$this->add('', 'Api::index');
		$this->add('/', 'Api::index');

		$this->add('/login/patient', 'Api::authenticatePatient');
		$this->add('/login/doctor', 'Api::authenticateDoctor');

		$this->add('/doctor', 'Api::getDoctor');

		$this->add('/appointment', 'Api::getAppointment');
		$this->add('/appointment/add', 'Api::addAppointment');
		$this->add('/appointment/add/result', 'Api::addAppointmentResult');
		$this->add('/appointment/add/result/questions', 'Api::addAppointmentResultQuestions');

		$this->add('/patient', 'Api::getPatient');
		$this->add('/patient/add', 'Api::addPatient');
		$this->add('/patient/appointments', 'Api::getPatientAppointments');
	}
}
