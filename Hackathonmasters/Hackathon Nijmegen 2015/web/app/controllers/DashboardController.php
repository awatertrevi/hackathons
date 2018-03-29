<?php

/**
 * Namespace
 * @copyright (c) 2014 - 2015 | Paradoxis
 */
namespace Paradoxis\Controllers;

/**
 * Use classes
 */
use Paradoxis\Models\Appointments;
use \Paradoxis\Models\Patients;

/**
 * Class DashboardController
 * @version 0.1.0
 * @package Paradoxis\Controllers
 * @author  Paradoxis <luke@paradoxis.nl>
 */
class DashboardController extends ControllerBase {

	/**
	 * On initialize
	 * @return void
	 */
	public function initialize() {
		parent::initialize();

		$this->assets->addJs('plugins/material.min.js');
		$this->assets->addJs('plugins/ripples.min.js');
		$this->assets->addJs('plugins/bootstrap-hover-dropdown.min.js');

		if (!$this->session->get('logged-in') && $this->request->getURI() != "/dashboard/login") {
			$this->response->redirect('dashboard/login');
			$this->response->send();
			exit;
		}
	}

	/**
	 * Home action for the doctor's dashboard
	 * @return void
	 */
	public function homeAction() {
		$this->assets->addJs('assets/js/home.js');
		$this->view->pick('pages/dashboard/home');
	}

	/**
	 * Patient action for the doctor's dashboard
	 * @return void
	 */
	public function patientsAction() {
		$this->assets->addJs('assets/js/home.js');
		$this->view->pick('pages/dashboard/patients');
		$this->view->setVar('patients', Patients::find());
	}

	/**
	 * Appointments action for the doctors dash
	 * @return void
	 */
	public function appointmentsAction() {
		$this->assets->addJs('assets/js/appointments.js');
		$this->view->pick('pages/dashboard/appointments');
		$this->view->setVar('appointments', Appointments::find(["order" => "appointmentDate DESC"]));
		$this->view->setVar('patients', Patients::find());
	}

	/**
	 * Login action for the doctor's dashboard
	 * @return void
	 */
	public function loginAction() {
		$this->assets->addCSs('assets/css/login.css');
		$this->assets->addJs('assets/js/login.js');
		$this->view->pick('pages/dashboard/login');
	}
}