<?php

namespace Hackathon\Routes;

use \Phalcon\Mvc\Router\Group;

/**
 * Class Api
 *
 * @package Hackathon\Routes
 * @author  Trevi Awater <trevi@modision.com>
 */
class Api extends Group
{
	/**
	 * Constructor
	 *
	 * @return void
	 */
	public function initialize()
	{
		$this->setPrefix('/api');

		$this->add('', 'Api::index');
		$this->add('/', 'Api::index');

		$this->add('/presentations', 'Api::listPresentations');
		$this->add('/cities', 'Api::listCities');
		$this->add('/presentors', 'Api::getPresentor');
	}
}
