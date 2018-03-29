<?php

namespace Hackathon\Models;

class Presentations extends \Phalcon\Mvc\Model
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
    public $title;

    /**
     *
     * @var string
     */
    public $city;

    /**
     *
     * @var string
     */
    public $time;

    /**
     *
     * @var integer
     */
    public $presenterId;

    /**
     *
     * @var string
     */
    public $building;

    /**
     *
     * @var string
     */
    public $distance;

    /**
     *
     * @var string
     */
    public $content;

    /**
     *
     * @var string
     */
    public $imageUrl;

    /**
     * Initialize method for model.
     */
    public function initialize()
    {
        $this->belongsTo('presenterId', 'Presentors', 'id', array('alias' => 'Presentors'));
    }

    /**
     * Returns table name mapped in the model.
     *
     * @return string
     */
    public function getSource()
    {
        return 'presentations';
    }

    /**
     * Allows to query a set of records that match the specified conditions
     *
     * @param mixed $parameters
     * @return Presentations[]
     */
    public static function find($parameters = null)
    {
        return parent::find($parameters);
    }

    /**
     * Allows to query the first record that match the specified conditions
     *
     * @param mixed $parameters
     * @return Presentations
     */
    public static function findFirst($parameters = null)
    {
        return parent::findFirst($parameters);
    }

}
