﻿using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace me.cg360.spookums.utility
{
    /**
     * A thread wrapper that ensures the thread is killed when
     * the game ends.
     */
    public class GameThread
    {

        private static bool _operating;
        private static Thread _watcherThread;
        private static List<Thread> _threadList;

        static GameThread()
        {
            _threadList = new List<Thread>();
            _watcherThread = new Thread(() =>
            {
                _operating = true;
                while(Application.isPlaying) { }

                _operating = false;
                foreach (Thread thread in _threadList)
                {
                    try
                    {
                        thread.Interrupt();
                    }
                    catch (Exception err)
                    {
                        Debug.LogException(err);
                    }
                }
            });
            
            _threadList = new List<Thread>();
        }


        public Thread Thread { get; }

        public GameThread(ThreadStart threadStart)
        {
            Thread = new Thread(threadStart);
            lock (_threadList)
            {
                _threadList.Add(Thread);
            }
        }

        public void Start()
        {
            if(_operating) Thread.Start();
        }
        
        public void Join()
        {
            if(_operating) Thread.Join();
        }

        public void Interrupt()
        {
            Thread.Interrupt();
        }
        
        public static void StartThreadChecking()
        {
            _watcherThread.Start();
        }
        
    }
}