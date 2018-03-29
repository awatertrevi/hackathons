<?php

namespace Paradoxis\Models;

use Phalcon\Mvc\Model\Validator\Email as Email;

class Patients extends \Phalcon\Mvc\Model
{

    /**
     *
     * @var integer
     */
    public $id;

    /**
     *
     * @var string
     */
	public $firstname;
	public $lastname;

    /**
     *
     * @var string
     */
    public $email;

    /**
     *
     * @var string
     */
    public $password;

    /**
     *
     * @var string
     */
    public $phone;

    /**
     *
     * @var string
     */
    public $birthday;

    /**
     *
     * @var string
     */
    public $insurance;

    /**
     *
     * @var double
     */
    public $risk;

    /**
     *
     * @var string
     */
    public $zip;

    /**
     *
     * @var string
     */
    public $house;

    /**
     * Validations and business logic
     *
     * @return boolean
     */
    public function validation()
    {
        $this->validate(
            new Email(
                array(
                    'field'    => 'email',
                    'required' => true,
                )
            )
        );

        if ($this->validationHasFailed() == true) {
            return false;
        }

        return true;
    }

    /**
     * Initialize method for model.
     */
    public function initialize()
    {
        $this->hasMany('id', '\Paradoxis\Models\Appointments', 'patientId', array('alias' => 'Appointments'));
        $this->hasMany('id', '\Paradoxis\Models\PatientRelatives', 'patientId', array('alias' => 'PatientRelatives'));
    }

    /**
     * Returns table name mapped in the model.
     *
     * @return string
     */
    public function getSource()
    {
        return 'patients';
    }

    /**
     * Allows to query a set of records that match the specified conditions
     *
     * @param mixed $parameters
     * @return Patients[]
     */
    public static function find($parameters = null)
    {
        return parent::find($parameters);
    }

    /**
     * Allows to query the first record that match the specified conditions
     *
     * @param mixed $parameters
     * @return Patients
     */
    public static function findFirst($parameters = null)
    {
        return parent::findFirst($parameters);
    }

}
