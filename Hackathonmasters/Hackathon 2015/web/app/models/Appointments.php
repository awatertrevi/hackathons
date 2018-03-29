<?php

namespace Paradoxis\Models;

class Appointments extends \Phalcon\Mvc\Model
{

    /**
     *
     * @var integer
     */
    public $id;

    /**
     *
     * @var integer
     */
    public $doctorId;

    /**
     *
     * @var integer
     */
    public $patientId;

    /**
     *
     * @var string
     */
    public $subject;

    /**
     *
     * @var string
     */
    public $symptoms;

    /**
     *
     * @var string
     */
    public $appointmentDate;

    /**
     * Initialize method for model.
     */
    public function initialize()
    {
        $this->hasMany('id', '\Paradoxis\Models\AppointmentResults', 'appointmentId', array('alias' => 'AppointmentResults'));
        $this->belongsTo('doctorId', '\Paradoxis\Models\Doctors', 'id', array('alias' => 'Doctors'));
        $this->belongsTo('patientId', '\Paradoxis\Models\Patients', 'id', array('alias' => 'Patients'));
    }

    /**
     * Returns table name mapped in the model.
     *
     * @return string
     */
    public function getSource()
    {
        return 'appointments';
    }

    /**
     * Allows to query a set of records that match the specified conditions
     *
     * @param mixed $parameters
     * @return Appointments[]
     */
    public static function find($parameters = null)
    {
        return parent::find($parameters);
    }

    /**
     * Allows to query the first record that match the specified conditions
     *
     * @param mixed $parameters
     * @return Appointments
     */
    public static function findFirst($parameters = null)
    {
        return parent::findFirst($parameters);
    }

}
