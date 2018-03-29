<?php

/**
 * Namespace
 * @copyright (c) 2014 - 2015 | Paradoxis
 */
namespace Paradoxis\Controllers;

/**
 * Use classes
 */
use Paradoxis\Models\AppointmentResultQuestions;
use Paradoxis\Models\AppointmentResults;
use Paradoxis\Models\Appointments;
use Paradoxis\Models\Doctors;
use \Paradoxis\Models\Patients;
use Phalcon\Annotations\Exception;

/**
 * Class ApiController
 * @version 0.1.0
 * @package Paradoxis\Controllers
 * @author  Paradoxis <luke@paradoxis.nl>
 */
class ApiController extends ControllerBase {

	/**
	 * Error codes
	 * @var string
	 */
	const ERR_NO_CALL = "No API call specified";
	const ERR_INVALID_CALL = "Invalid API call (404)";
	const ERR_INVALID_REQUEST = "Invalid request (400)";

	/**
	 * Initialize component
	 * @return void
	 */
	public function initialize() {
		parent::initialize();
	}

	/**
	 * Default route "/api"
	 * @return void
	 */
	public function indexAction() {
		$this->fail(self::ERR_NO_CALL);
	}

	/**
	 * Authenticate a patient
	 * @return void
	 */
	public function authenticatePatientAction() {
		if ($this->request->has('email') == false || $this->request->has('password') == false) {
			$this->fail(self::ERR_INVALID_REQUEST);
			return;
		}

		if ($user = Patients::findFirst([
			"conditions" => "email = :email: AND password = :password:",
			"bind" => [
				"email" => $this->request->get('email'),
				"password" => $this->request->get('password')
			]
		])) {
			$patient = (object) $user->dump();
			unset($patient->password);
			$this->session->set('logged-in', true);
			$this->session->set('is-doctor', false);
			$this->success($patient, "Login successful");
		} else {
			$this->fail("Invalid username or password");
		}
	}

	/**
	 * Authenticate a doctor
	 * @return void
	 */
	public function authenticateDoctorAction() {
		if ($this->request->has('email') == false || $this->request->has('password') == false) {
			$this->fail(self::ERR_INVALID_REQUEST);
			return;
		}

		if ($user = Doctors::findFirst([
			"conditions" => "email = :email: AND password = :password:",
			"bind" => [
				"email" => $this->request->get('email'),
				"password" => $this->request->get('password')
			]
		])) {
			$patient = (object) $user->dump();
			unset($patient->password);
			$this->session->set('logged-in', true);
			$this->session->set('is-doctor', true);
			$this->session->set('user', $patient);
			$this->success($patient, "Login successful");
		} else {
			$this->fail("Invalid username or password");
		}
	}

	/**
	 * Get a patient's user data
	 * @return void
	 */
	public function getPatientAction() {
		if ($this->request->has('id') == false) {
			$this->fail(self::ERR_INVALID_REQUEST);
			return;
		}

		if ($user = (object) Patients::findFirst($this->request->get('id'))->toArray()) {
			unset($user->password);
			$this->success($user);
		} else {
			$this->fail("Invalid patient id");
		}
	}

	/**
	 * Get a doctor's user data
	 * @return void
	 */
	public function getDoctorAction() {
		if ($this->request->has('id') == false) {
			$this->fail(self::ERR_INVALID_REQUEST);
			return;
		}

		if ($doctor = (object) Doctors::findFirst($this->request->get('id'))->toArray()) {
			unset($doctor->password);
			$this->success($doctor);
		} else {
			$this->fail("Invalid doctor id");
		}
	}

	/**
	 * Get appointment
	 * @return void
	 */
	public function getAppointmentAction() {
		if ($this->request->has('id') == false) {
			$this->fail(self::ERR_INVALID_REQUEST);
			return;
		}

		if ($appointments = Appointments::findFirst($this->request->get('id'))) {
			$this->success($appointments->toArray());
		} else {
			$this->fail("Invalid appointment id");
		}
	}

	/**
	 * Get a patient's appointments by patientId
	 * @return void
	 */
	public function getPatientAppointmentsAction() {
		if ($this->request->has('id') == false) {
			$this->fail(self::ERR_INVALID_REQUEST);
			return;
		}

		if ($user = Patients::findFirst($this->request->get('id'))) {
			$this->success($user->getAppointments()->toArray());
		} else {
			$this->fail("Invalid patient id");
		}
	}

	/**
	 * Add an appointment result
	 * @return void
	 */
	public function addAppointmentResultAction() {
		if ($this->request->has('id') == false || $this->request->has('prescription') == false) {
			$this->fail(self::ERR_INVALID_REQUEST);
			return;
		}

		$appointmentResult = new AppointmentResults();
		$appointmentResult->appointmentId = $this->request->get('id');
		$appointmentResult->prescription = $this->request->get('prescription');

		if ($appointmentResult->save()) {
			$this->success($appointmentResult->id);
		} else {
			$this->fail("Unable to save");
		}
	}

	/**
	 * Add an appointment result question set
	 * @return void
	 */
	public function addAppointmentResultQuestionsAction() {
		if ($this->request->has('id') == false || $this->request->has('questions') == false) {
			$this->fail(self::ERR_INVALID_REQUEST);
			return;
		}

		if ($appointment = AppointmentResults::findFirst([
			"conditions" => "id = :id:",
			"bind" => [
				"id" => $this->request->get('id')
			]
		])) {
			$question = $this->request->get('questions');

			for($i = 0; $i < 3; $i++) {
				$aQuestions = new AppointmentResultQuestions();
				$aQuestions->appointmentId = $this->request->get('id');
				$aQuestions->question = $question[$i]['question'];
				$aQuestions->answer = $question[$i]['answer'];
				if(!$aQuestions->save()) {
					throw new Exception(print_r($aQuestions->getMessages(), true));
				}
			}

			$this->success();
		} else {
			$this->fail("Invalid appointment ID");
			exit;
		}
	}

	/**
	 * Get the questions related to the appointments
	 * @return void
	 */
	public function getAppointmentQuestionsAction() {
		if ($this->request->has('id') == false) {
			$this->fail(self::ERR_INVALID_REQUEST);
			return;
		}

		if ($questions = AppointmentResultQuestions::find([
			"conditions" => "appointmentId = :id:",
			"bind" => [
				"id" => $this->request->get('id')
			]
		])) {
			$this->success($questions);
		} else {
			$this->fail("Invalid appointment ID");
		}
	}

	/**
	 * Add an appointment
	 * @return void
	 */
	public function addAppointmentAction() {
		if (
			$this->request->has('name') == false ||
			$this->request->has('subject') == false ||
			$this->request->has('symptoms') == false ||
			$this->request->has('date') == false
		) {
			$this->fail(self::ERR_INVALID_REQUEST);
			return;
		}

		if (Patients::findFirst($this->request->get('name'))) {
			$appointment = new Appointments();
			$appointment->doctorId = $this->session->get('user')->id;
			$appointment->patientId = $this->request->get('name');
			$appointment->subject = $this->request->get('subject');
			$appointment->symptoms = $this->request->get('symptoms');
			$appointment->appointmentDate = $this->request->get('date');

			if ($appointment->save()) {
				$this->success();
			} else {
				$this->fail("Unable to save appointment");
			}
		} else {
			$this->fail("Invalid name");
		}
	}

	/**
	 * Add a patient
	 * @return void
	 */
	public function addPatientAction() {
		if (
			$this->request->has('firstname') == false ||
			$this->request->has('lastname') == false ||
			$this->request->has('email') == false ||
			$this->request->has('password') == false ||
			$this->request->has('phone') == false ||
			$this->request->has('birthday') == false ||
			$this->request->has('insurance') == false ||
			$this->request->has('risk') == false ||
			$this->request->has('zip') == false ||
			$this->request->has('house') == false
		) {
			$this->fail(self::ERR_INVALID_REQUEST);
			return;
		}

		$patient = new Patients();
		$patient->firstname = $this->request->get('firstname');
		$patient->lastname = $this->request->get('lastname');
		$patient->email = $this->request->get('email');
		$patient->password = $this->request->get('password');
		$patient->phone = $this->request->get('phone');
		$patient->birthday = $this->request->get('birthday');
		$patient->insurance = $this->request->get('insurance');
		$patient->risk = $this->request->get('risk');
		$patient->zip = $this->request->get('zip');
		$patient->house = $this->request->get('house');

		if ($patient->save()) {
			$this->success();
		} else {
			$this->fail("Unable to save patient");
		}
	}

	/**
	 * Add a question, must be linked with an appointment
	 * @return void
	 */
	public function addAppointmentResultQuestionAction() {
		if (
			$this->request->has('question') == false ||
			$this->request->has('answer') == false ||
			$this->request->has('id') == false
		) {
			$this->fail(self::ERR_INVALID_REQUEST);
			return;
		}

		if ($appointment = AppointmentResults::findFirst([
			"conditions" => "id = :id:",
			"bind" => [
				"id" => $this->request->get('id')
			]
		])) {
			$question = new AppointmentResultQuestions();
			$question->appointmentId = $this->request->get('id');
			$question->question = $this->request->get('question');
			$question->answer = $this->request->get('answer');

			if ($question->save() == false) {
				$this->success();
			} else {
				$this->fail("Unable to save question");
			}
		} else {
			$this->fail("Invalid appointment ID");
			exit;
		}
	}

	/**
	 * Get appointment question answer
	 * @return void
	 */
	public function getAppointmentAnswerAction() {
		if ($this->request->has('id') == false) {
			$this->fail(self::ERR_INVALID_REQUEST);
			return;
		}

		if ($answer = AppointmentResultQuestions::findFirst($this->request->get('id'))->answer) {
			$this->success($answer);
		} else {
			$this->fail('Invalid question id');
		}
	}
}
