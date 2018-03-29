<?php

namespace Paradoxis\Models;

class AppointmentResults extends \Phalcon\Mvc\Model
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
    public $appointmentId;

    /**
     *
     * @var string
     */
    public $prescription;

    /**
     *
     * @var string
     */
    public $nextAppointment;

    /**
     * Initialize method for model.
     */
    public function initialize()
    {
        $this->hasMany('id', '\Paradoxis\Models\AppointmentResultQuestions', 'appointmentId', array('alias' => 'AppointmentResultQuestions'));
        $this->belongsTo('appointmentId', '\Paradoxis\Models\Appointments', 'id', array('alias' => 'Appointments'));
    }

    /**
     * Returns table name mapped in the model.
     *
     * @return string
     */
    public function getSource()
    {
        return 'appointmentResults';
    }

    /**
     * Allows to query a set of records that match the specified conditions
     *
     * @param mixed $parameters
     * @return AppointmentResults[]
     */
    public static function find($parameters = null)
    {
        return parent::find($parameters);
    }

    /**
     * Allows to query the first record that match the specified conditions
     *
     * @param mixed $parameters
     * @return Appointmentresults
     */
    public static function findFirst($parameters = null)
    {
        return parent::findFirst($parameters);
    }

}
