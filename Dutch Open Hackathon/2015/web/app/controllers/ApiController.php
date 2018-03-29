<?php

namespace Hackathon\Controllers;

use Phalcon\Annotations\Exception;
use Phalcon\Mvc\Model\Query;

use Hackathon\Models\Presentations;
use Hackathon\Models\Presentors;

/**
 * Class ApiController
 *
 * @package Hackathon\Controllers
 * @author  Trevi Awater <trevi@modision.com>
 */
class ApiController extends ControllerBase
{
   // Declaring some custom error messages.
   const ERR_NO_CALL = "No api call specified.";
   const ERR_INVALID_CALL = "Invalid api call.";
   const ERR_INVALID_REQUEST = "Invalid api request.";
   const ERR_NOT_AUTHORIZED = "Not authorized.";


   /**
    * This is fired when no api call is made.
    *
    * @return void
    */
   public function indexAction()
   {
      $this->fail(self::ERR_NO_CALL);
   }

   public function listPresentationsAction()
   {
      if ($this->request->has('city') == false)
      {
         $this->fail(self::ERR_INVALID_REQUEST);

         return;
      }

      $presentations = Presentations::find(
      [
   		"conditions" => "city = :city:",
   		"bind" =>
         [
   		   "city" => $this->request->get('city')
         ]
      ]);

      $this->success(array('presentations' => $presentations->toArray()));
   }

   public function listCitiesAction()
   {
      $query = new Query("SELECT DISTINCT city FROM Hackathon\Models\Presentations", $this->getDI());

      $this->success(array('cities' => $query->execute()->toArray()));
   }

   public function getPresentorAction()
   {
      if ($this->request->has('id') == false)
      {
         $this->fail(self::ERR_INVALID_REQUEST);

         return;
      }

      $presentor = Presentors::findFirst(
      [
   		"conditions" => "id = :id:",
   		"bind" =>
         [
   		   "id" => $this->request->get('id')
         ]
      ]);

      $this->success($presentor);
   }
}
