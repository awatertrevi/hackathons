<?php

namespace Hackathon\Models;

use Phalcon\Mvc\Model\Validator\Email as Email;

class Presentors extends \Phalcon\Mvc\Model
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
    public $name;

    /**
     *
     * @var string
     */
    public $company;

    /**
     *
     * @var string
     */
    public $email;

    /**
     *
     * @var string
     */
    public $twitter;

    /**
     *
     * @var string
     */
    public $linkedIn;

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
        $this->hasMany('id', 'Presentations', 'presenterId', array('alias' => 'Presentations'));
    }

    /**
     * Returns table name mapped in the model.
     *
     * @return string
     */
    public function getSource()
    {
        return 'presentors';
    }

    /**
     * Allows to query a set of records that match the specified conditions
     *
     * @param mixed $parameters
     * @return Presentors[]
     */
    public static function find($parameters = null)
    {
        return parent::find($parameters);
    }

    /**
     * Allows to query the first record that match the specified conditions
     *
     * @param mixed $parameters
     * @return Presentors
     */
    public static function findFirst($parameters = null)
    {
        return parent::findFirst($parameters);
    }

}
