<?php

namespace Paradoxis\Models;

class AppointmentResultQuestions extends \Phalcon\Mvc\Model
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
    public $question;

    /**
     *
     * @var string
     */
    public $answer;

    /**
     * Initialize method for model.
     */
    public function initialize()
    {
        $this->belongsTo('appointmentId', '\Paradoxis\Models\AppointmentResults', 'id', array('alias' => 'AppointmentResults'));
    }

    /**
     * Returns table name mapped in the model.
     *
     * @return string
     */
    public function getSource()
    {
        return 'appointmentResultQuestions';
    }

    /**
     * Allows to query a set of records that match the specified conditions
     *
     * @param mixed $parameters
     * @return AppointmentResultQuestions[]
     */
    public static function find($parameters = null)
    {
        return parent::find($parameters);
    }

    /**
     * Allows to query the first record that match the specified conditions
     *
     * @param mixed $parameters
     * @return AppointmentResultQuestions
     */
    public static function findFirst($parameters = null)
    {
        return parent::findFirst($parameters);
    }

}
